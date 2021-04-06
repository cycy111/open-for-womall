using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProvideForUnionAPI.Dto.Request;
using ProvideForUnionAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TmsOpenForWoMall.Dto.Request.Union;
using TmsOpenForWoMall.Dto.Response.Union;
using TmsOpenForWoMall.Infrastructure.DbCommon;
using Zh.Common.ApiRespose;
using Zh.Common.Cryptography;
using Zh.Common.DbHelper;

namespace TmsOpenForWoMall.Dal.CheckDal
{
    /// <summary>
    /// 联通商场数据访问
    /// </summary>
    public class UnionDal
    {
        //利用接口实现注入
        private IUnionDbConnection _unDbConnection;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unDbConnection"></param>
        public UnionDal(
            IUnionDbConnection unDbConnection)
        {
            _unDbConnection = unDbConnection;
        }

        /// <summary>
        /// 记录调用获取Access token接口
        /// </summary>
        /// <param name="reqTokenDto"></param>
        /// <param name="httpContext"></param>
        public async Task<ResModel<ResTokenDto>> GetToken(ReqTokenDto reqTokenDto, HttpContext httpContext)
        {
            ResModel<ResTokenDto> res = new ResModel<ResTokenDto>();
            ResTokenDto token = new ResTokenDto();
            string strConn = _unDbConnection.GetDbConnStr();

            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //记录接口调用
                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'GetToken ','ReqTokenDto;HttpContext',@ip,getdate()";
                    await conn.ExecuteAsync(sqlstr, new { ip = ipStr }, transaction);
                    transaction.Commit();
                    conn.Close();
                    res.success = "true";
                    return res;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    res.success = "false";
                    res.resultMessage = "推送消息失败：" + ex.Message;
                    return res;
                }
            }
                
            
        }
        /// <summary>
        /// 平台根据物流单查询当前物流单的物流配送信息
        /// </summary>
        /// <param name="orderNo">物流单</param>
        /// <param name="httpContext">请求上下文</param>
        /// <returns></returns>
        public async Task<ResModel<JObject>> GetLogiticOrderByNo(string orderNo, HttpContext httpContext)
        {
            
            var res = new ResModel<JObject>();
            if (orderNo == "")
            {
                res.success = "false";
                res.resultMessage = "logiticOrderNo为空";
                res.resultCode = "";
                return res;
            }
            var resOrderInfoDto = new ResOrderInfoDto();
            res.success = "true";
            resOrderInfoDto.logisticsNo = orderNo;

            var orderTtrackList = new List<orderTrack>();

            DynamicParameters dp = new DynamicParameters();

            string strConn = _unDbConnection.GetDbConnStr();
            //using (var conn = GetOpenConnection(strConn, DbProvider.SqlServer)) //不同版本
            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //插入接口调用记录
                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'GetLogiticOrderByNo','orderNo;httpContext',@ip,getdate()";
                    var rst=  conn.Execute(sqlstr, new { ip=ipStr },transaction);

                    sqlstr = @"select MsgTime,Content from T_ORDERTRACK where OrderNo=@logisticsNo";
                    var result =await conn.QueryAsync<orderTrack>(sqlstr, new { logisticsNo=orderNo }, transaction);
                    resOrderInfoDto.orderTracks = result.ToList();

                    sqlstr = @"select top 1 isDelivered from T_ORDERTRACK where OrderNo=@logisticsNo order by MsgTime desc";
                    resOrderInfoDto.isDelivered = await conn.ExecuteScalarAsync<string>(sqlstr, new { logisticsNo=orderNo }, transaction);

                    transaction.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    res.success = "false";
                    res.resultMessage = ex.Message;
                    res.resultCode = "";
                    return res;
                }
            }
            res.result = JObject.FromObject(resOrderInfoDto);
            res.success = "true";
            return res;
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="addMsg"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<ResModel<JObject>> getOrderPushMsg(ReqPushMsgDto reqPushMsgDto, HttpContext httpContext) {
            ResModel<JObject> res = new ResModel<JObject>();
            res.success = "success";
            res.resultMessage = "推送消息成功！";

            if (string.IsNullOrEmpty(reqPushMsgDto.msgInfo))
            {
                res.success = "false";
                res.resultMessage = "msgInfo为空";
                res.resultCode = "";
                return res;
            }
            if (string.IsNullOrEmpty(reqPushMsgDto.type))
            {
                res.success = "false";
                res.resultMessage = "type为空";
                res.resultCode = "";
                return res;
            }
            string strConn = _unDbConnection.GetDbConnStr();
            
            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
            {
                IDbTransaction transaction = conn.BeginTransaction();
                if (reqPushMsgDto.type == "2")     //物流订单确认
                {
                    try
                    {
                        var id = 0;
                        var obj = JsonConvert.DeserializeObject<CfmMsg>(reqPushMsgDto.msgInfo);
                        //记录接口调用
                        string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                        string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'LogiticOrderConfirm ','ReqPushCfmMsgDto;HttpContext',@ip,getdate()";
                        await conn.ExecuteAsync(sqlstr, new { ip = ipStr }, transaction);
                        
                        //确认消息的情况：记录推送过来的消息
                        if (obj.stype == "1")
                        {
                            //总价格，stype=1时为必填
                            if (string.IsNullOrEmpty(obj.totalPrice))
                            {
                                res.success = "false";
                                res.resultMessage = "总价格为空！";
                                res.result = JObject.FromObject( new ResMsgDto() { msg = "推送消息失败!" });
                                return res;
                            }
                            else
                            {
                                sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,TotalPrice,AddDate,EleContractNo,MsgType,needReply) 
select @orderNo,2,@msgTime,@totalPrice,getdate(),@eleContractNo,1,@needReply; SELECT CAST(SCOPE_IDENTITY() as int)";

                                id = await conn.QueryFirstAsync<int>(sqlstr, new
                                {
                                    orderNo = obj.logisticsOrderNo,
                                    msgTime = obj.time,
                                    totalPrice = obj.totalPrice,
                                    eleContractNo = obj.eleContractNo,
                                    needReply=obj.needReply
                                }, transaction);
                            }

                        }
                        else if (obj.stype == "2") //取消
                        {
                            sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,AddDate,MsgType,needReply) 
select @orderNo,2,@msgTime,getdate(),2,@needReply; 
SELECT CAST(SCOPE_IDENTITY() as int)";

                            id = await conn.QueryFirstAsync<int>(sqlstr, new { orderNo = obj.logisticsOrderNo, msgTime = obj.time, needReply=obj.needReply }, transaction);
                        }
                        //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
                        sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.8','物流订单接收确认接口',@id,0";
                        await conn.ExecuteAsync(sqlstr, new { id = obj.logisticsOrderNo }, transaction);

                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息成功!" });

                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        res.success = "false";
                        res.resultMessage = "推送消息失败：" + ex.Message;
                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息失败！" });

                    }
                }
                else if (reqPushMsgDto.type == "3")  //新增追加费用
                {
                    try
                    {
                        var id = 0;
                        var obj = JsonConvert.DeserializeObject<AddExpMsg>(reqPushMsgDto.msgInfo);

                        //记录接口调用
                        string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                        string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'LogiticOrderAddExpense ','ReqPushAddExpMsgDto;HttpContext',@ip,getdate()";
                        await conn.ExecuteAsync(sqlstr, new { ip = ipStr }, transaction);

                        //记录推送过来的消息
                        sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,AddedExpense,AddDate,Remark) 
select @orderNo,3,@msgTime,@addedExpense,getdate(),@remark; SELECT CAST(SCOPE_IDENTITY() as int)";

                        //消息在平台推送物流单消息表中的标识id
                        id = await conn.QueryFirstAsync<int>(sqlstr, new
                        {
                            orderNo = obj.logisticsOrderNo,
                            msgTime = obj.time,
                            addedExpense = obj.addedExpense,
                            remark = obj.remark,
                        }, transaction);

                        //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
                        sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.13','查看追加费用接口',@id,0";
                        await conn.ExecuteAsync(sqlstr, new { id = obj.logisticsOrderNo }, transaction);

                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息成功!" });

                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        res.success = "false";
                        res.resultMessage = "推送消息失败：" + ex.Message;
                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息失败！" });

                    }
                }
                else if (reqPushMsgDto.type == "6")  //妥投驳回
                {
                    try
                    {
                        var id = 0;
                        var obj = JsonConvert.DeserializeObject<DelivRejMsg>(reqPushMsgDto.msgInfo);

                        //记录接口调用
                        string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                        string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'LogiticOrderDelivRej ','ReqPushDelivRejMsgDto;HttpContext',@ip,getdate()";
                        await conn.ExecuteAsync(sqlstr, new { ip = ipStr }, transaction);

                        //记录推送过来的消息
                        sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,DeliveredType,DeliveredInfo,DeliveredPerson,AddDate) 
select @orderNo,6,@time,2,@deliveredInfo,@deliveredPerson,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";

                        //消息在平台推送物流单消息表中的标识id
                        id = await conn.QueryFirstAsync<int>(sqlstr, new
                        {
                            orderNo = obj.logisticsOrderNo,
                            time = obj.time,
                            deliveredInfo = obj.deliveredInfo,
                            deliveredPerson = obj.deliveredPerson,
                        }, transaction);

                        //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
                        sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.16','妥投/拒收信息推送接口',@id,0";
                        await conn.ExecuteAsync(sqlstr, new { id = obj.logisticsOrderNo }, transaction);

                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息成功!" });

                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        res.success = "false";
                        res.resultMessage = "推送消息失败：" + ex.Message;
                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息失败！" });

                    }
                }
                else if (reqPushMsgDto.type == "7") //线路价格更新审批结果推送
                {
                    try
                    {
                        var id = 0;
                        var obj = JsonConvert.DeserializeObject<PriceMsg>(reqPushMsgDto.msgInfo);

                        //记录接口调用
                        string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                        string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'RoutPriceUpdate','ReqPushRoutPriceMsgDto;HttpContext',@ip,getdate()";
                        await conn.ExecuteAsync(sqlstr, new { ip = ipStr }, transaction);

                        //记录推送过来的消息
                        sqlstr = @"insert into T_PUSHMSG (Types,RouteAgreementNo,ApplyNO,CheckState,MsgTime,remark,AddDate) 
select 7,@routeAgreementNo,@applyNO,@checkState,@time,@remark,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";

                        //消息在平台推送物流单消息表中的标识id
                        id = await conn.QueryFirstAsync<int>(sqlstr, new
                        {
                            routeAgreementNo = obj.routeAgreementNo,
                            applyNO = obj.applyNo,
                            checkState = obj.status,
                            time = obj.time,
                            remark = obj.remark
                        }, transaction);

                        DynamicParameters dp = new DynamicParameters();

                        dp.Add("@ApplyNo", obj.applyNo);
                        dp.Add("@Status", obj.status);

                        //审批未通过时,找到申请单号对应的价格表更新的审批状态；审批通过时,不存在则新增,存在则更新
                        var result = await conn.ExecuteScalarAsync<string>("UN_CheckRoutPrice", dp, transaction, commandType: CommandType.StoredProcedure);
                        if (result.ToString() == "1")
                            res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息成功!" });

                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        res.success = "false";
                        res.resultMessage = "推送消息失败：" + ex.Message;
                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息失败！" });

                    }
                }
                else if (reqPushMsgDto.type == "8") //追加费用确认
                {
                    try
                    {
                        var id = 0;
                        var obj = JsonConvert.DeserializeObject<pactMsg>(reqPushMsgDto.msgInfo);

                        //记录接口调用
                        string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                        string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'LogiticOrderAddExpComf','ReqPushAddExpCofmMsgDto;HttpContext',@ip,getdate()";
                        await conn.ExecuteAsync(sqlstr, new { ip = ipStr }, transaction);

                        //记录推送过来的消息
                        sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,NeedEleContract,EleContractNo,AddDate) 
select @orderNo,8,@time,@needEleContract,@eleContractNo,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";

                        //消息在平台推送物流单消息表中的标识id
                        id = await conn.QueryFirstAsync<int>(sqlstr, new
                        {
                            orderNo = obj.logisticsOrderNo,
                            time = obj.time,
                            needEleContract = obj.needEleContract,
                            eleContractNo = obj.eleContractNo,
                        }, transaction);

                        //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
                        sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.11','电子合同查询接口',@id,0";
                        await conn.ExecuteAsync(sqlstr, new { id = obj.logisticsOrderNo }, transaction);

                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息成功!" });

                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        res.success = "false";
                        res.resultMessage = "推送消息失败：" + ex.Message;
                        res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息失败！" });

                    }
                }
            }
            res.success = "true";
            return res;
        }

        /// <summary>
        /// 推送新增物流订单消息 
        /// </summary>
        /// <param name="httpContext">请求上下文</param>
        /// <param name="addMsg">消息实体</param>
        /// <returns></returns>
        public async Task<ResModel<JObject>> AddLogiticOrder(AddMsg addMsg,HttpContext httpContext)
        {
            ResModel<JObject> res = new ResModel<JObject>();
            res.success = "success";
            res.resultMessage = "推送消息成功！";
            var type = addMsg.stype; //test
            string strConn = _unDbConnection.GetDbConnStr();
            
            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                { 
                    //记录接口调用
                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
                            select 'AddLogiticOrder ','addMsg;httpContext',@ip,getdate()";
                    await conn.ExecuteAsync(sqlstr, new { ip=ipStr },transaction);

                    //记录推送过来的消息
                    sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgType,MsgTime,AddDate) select @orderNo,1,@msgType,@msgTime,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";
                    var pushID = await conn.QueryFirstAsync<int>(sqlstr, new { orderNo=addMsg.logisticsOrderNo, msgType=addMsg.stype, msgTime =addMsg.time },transaction); ;

                    //插入记录到消息队列中
                    sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.7','物流订单查询接口',@id,0; SELECT CAST(SCOPE_IDENTITY() as int)";
                    await conn.ExecuteAsync(sqlstr, new {id = addMsg.logisticsOrderNo },transaction);
                    res.result = JObject.FromObject(new ResMsgDto() { msg = "推送消息成功!" });

                    transaction.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    res.success = "false";
                    res.resultMessage = "推送消息失败：" + ex.Message;

                }
            }
            res.success = "true";
            return res;
        }

//        /// <summary>
//        /// 推送物流订单确认消息
//        /// </summary>
//        /// <param name="reqPushCfmMsgDto">确认请求</param>
//        /// <param name="httpContext">请求上下文</param>
//        /// <returns></returns>
//        public async Task<ResModel<ResMsgDto>> LogiticOrderConfirm(ReqPushCfmMsgDto reqPushCfmMsgDto, HttpContext httpContext)
//        {
//            ResModel<ResMsgDto> res = new ResModel<ResMsgDto>();
//            res.success = "success";
//            res.resultMessage = "推送消息成功！";

//            string strConn = _unDbConnection.GetDbConnStr();

//            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
//            {
//                IDbTransaction transaction = conn.BeginTransaction();
//                try
//                {
//                    var id =0;

//                    //记录接口调用
//                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
//                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
//                            select 'LogiticOrderConfirm ','ReqPushCfmMsgDto;HttpContext',@ip,getdate()";
//                    await conn.ExecuteAsync(sqlstr, new { ip=ipStr },transaction);

//                    //确认消息的情况：记录推送过来的消息
//                    if (reqPushCfmMsgDto.msgInfo.stype == "1")
//                    {
//                        //总价格，stype=1时为必填
//                        if (string.IsNullOrEmpty(reqPushCfmMsgDto.msgInfo.totalPrice))
//                        {
//                            res.success = "false";
//                            res.resultMessage = "总价格为空！";
//                            res.result = new ResMsgDto() { msg = "推送消息失败!" };
//                            return res;
//                        }
//                        else
//                        {
//                            sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,TotalPrice,AddDate,EleContractNo,MsgType) 
//select @orderNo,2,@msgTime,@totalPrice,getdate(),@eleContractNo,1; SELECT CAST(SCOPE_IDENTITY() as int)";
                           
//                            id= await conn.QueryFirstAsync<int>(sqlstr, new {
//                                orderNo=reqPushCfmMsgDto.msgInfo.logisticsOrderNo,
//                                msgTime=reqPushCfmMsgDto.msgInfo.time,
//                                totalPrice=reqPushCfmMsgDto.msgInfo.totalPrice,
//                                eleContractNo=reqPushCfmMsgDto.msgInfo.eleContractNo
//                            },transaction);  
//                        }

//                    }
//                    else if (reqPushCfmMsgDto.msgInfo.stype == "2") //取消
//                    {
//                        sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,TotalPrice,AddDate,MsgType) select @orderNo,2,@msgTime,getdate(),2; 
//SELECT CAST(SCOPE_IDENTITY() as int)";
//                        id = await conn.QueryFirstAsync<int>(sqlstr, new { orderNo=reqPushCfmMsgDto.msgInfo.logisticsOrderNo, msgTime=reqPushCfmMsgDto.msgInfo.time },transaction);
//                    }
//                    //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
//                    sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.8','物流订单接收确认接口',@id,0";
//                    await conn.ExecuteAsync(sqlstr, new {id= id },transaction);

//                    res.result = new ResMsgDto() { msg = "推送消息成功!" };

//                    transaction.Commit();
//                    conn.Close();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    res.success = "false";
//                    res.resultMessage = "推送消息失败：" + ex.Message;
//                    res.result = new ResMsgDto() { msg = "推送消息失败！" };

//                }
//            }
//            return res;
//        }

//        /// <summary>
//        /// 推送物流订单追加费用消息
//        /// </summary>
//        /// <param name="reqPushAddExpMsgDto">确认请求</param>
//        /// <param name="httpContext">请求上下文</param>
//        /// <returns></returns>
//        public async Task<ResModel<ResMsgDto>> LogiticOrderAddExpense(ReqPushAddExpMsgDto reqPushAddExpMsgDto, HttpContext httpContext)
//        {
//            ResModel<ResMsgDto> res = new ResModel<ResMsgDto>();
//            res.success = "success";
//            res.resultMessage = "推送消息成功！";

//            string strConn = _unDbConnection.GetDbConnStr();

//            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
//            {
//                IDbTransaction transaction = conn.BeginTransaction();
//                try
//                {
//                    var id = 0;

//                    //记录接口调用
//                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
//                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
//                            select 'LogiticOrderAddExpense ','ReqPushAddExpMsgDto;HttpContext',@ip,getdate()";
//                    await conn.ExecuteAsync(sqlstr, new { ip=ipStr },transaction);

//                    //记录推送过来的消息
//                    sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,AddedExpense,AddDate,Remark) 
//select @orderNo,3,@msgTime,@addedExpense,getdate(),@remark; SELECT CAST(SCOPE_IDENTITY() as int)";

//                    //消息在平台推送物流单消息表中的标识id
//                    id = await conn.QueryFirstAsync<int>(sqlstr, new
//                    {
//                        orderNo= reqPushAddExpMsgDto.msgInfo.logisticsOrderNo,
//                        msgTime= reqPushAddExpMsgDto.msgInfo.time,
//                        addedExpense= reqPushAddExpMsgDto.msgInfo.addedExpense,
//                        remark=reqPushAddExpMsgDto.msgInfo.remark,
//                    },transaction);

//                    //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
//                    sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.13','查看追加费用接口',@id,0";
//                    await conn.ExecuteAsync(sqlstr, new { id=id },transaction);

//                    res.result = new ResMsgDto() { msg = "推送消息成功!" };

//                    transaction.Commit();
//                    conn.Close();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    res.success = "false";
//                    res.resultMessage = "推送消息失败：" + ex.Message;
//                    res.result = new ResMsgDto() { msg = "推送消息失败！" };

//                }
//            }
//            return res;
//        }

//        /// <summary>
//        /// 推送物流订单妥投驳回消息
//        /// </summary>
//        /// <param name="reqPushDelivRejMsgDto">确认请求</param>
//        /// <param name="httpContext">请求上下文</param>
//        /// <returns></returns>
//        public async Task<ResModel<ResMsgDto>> LogiticOrderDelivRej(ReqPushDelivRejMsgDto reqPushDelivRejMsgDto, HttpContext httpContext)
//        {
//            ResModel<ResMsgDto> res = new ResModel<ResMsgDto>();
//            res.success = "success";
//            res.resultMessage = "推送消息成功！";

//            string strConn = _unDbConnection.GetDbConnStr();

//            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
//            {
//                IDbTransaction transaction = conn.BeginTransaction();
//                try
//                {
//                    var id = 0;

//                    //记录接口调用
//                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
//                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
//                            select 'LogiticOrderDelivRej ','ReqPushDelivRejMsgDto;HttpContext',@ip,getdate()";
//                    await conn.ExecuteAsync(sqlstr, new { ip=ipStr },transaction);

//                    //记录推送过来的消息
//                    sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,DeliveredType,DeliveredInfo,DeliveredPerson,AddDate) 
//select @orderNo,6,@time,2,@deliveredInfo,@deliveredPerson,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";

//                    //消息在平台推送物流单消息表中的标识id
//                    id = await conn.QueryFirstAsync<int>(sqlstr, new
//                    {
//                        orderNo=reqPushDelivRejMsgDto.msgInfo.logisticsOrderNo,
//                        time=reqPushDelivRejMsgDto.msgInfo.time,
//                        deliveredInfo= reqPushDelivRejMsgDto.msgInfo.deliveredInfo,
//                        deliveredPerson= reqPushDelivRejMsgDto.msgInfo.deliveredPerson,
//                    },transaction);

//                    //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
//                    sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.16','妥投/拒收信息推送接口',@id,0";
//                    await conn.ExecuteAsync(sqlstr, new { id=id },transaction);

//                    res.result = new ResMsgDto() { msg = "推送消息成功!" };

//                    transaction.Commit();
//                    conn.Close();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    res.success = "false";
//                    res.resultMessage = "推送消息失败：" + ex.Message;
//                    res.result = new ResMsgDto() { msg = "推送消息失败！" };

//                }
//            }
//            return res;
//        }

//        /// <summary>
//        /// 推送线路价格更新审批结果
//        /// </summary>
//        /// <param name="reqPushRoutPriceMsgDto">确认请求</param>
//        /// <param name="httpContext">请求上下文</param>
//        /// <returns></returns>
//        public async Task<ResModel<ResMsgDto>> RoutPriceUpdate(ReqPushRoutPriceMsgDto reqPushRoutPriceMsgDto, HttpContext httpContext)
//        {
//            ResModel<ResMsgDto> res = new ResModel<ResMsgDto>();
//            res.success = "success";
//            res.resultMessage = "推送消息成功！";

//            string strConn = _unDbConnection.GetDbConnStr();

//            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
//            {
//                IDbTransaction transaction = conn.BeginTransaction();
//                try
//                {
//                    var id = 0;

//                    //记录接口调用
//                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
//                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
//                            select 'RoutPriceUpdate','ReqPushRoutPriceMsgDto;HttpContext',@ip,getdate()";
//                    await conn.ExecuteAsync(sqlstr, new { ip=ipStr },transaction);

//                    //记录推送过来的消息
//                    sqlstr = @"insert into T_PUSHMSG (Types,RouteAgreementNo,ApplyNO,CheckState,MsgTime,remark,AddDate) 
//select 7,@routeAgreementNo,@applyNO,@checkState,@time,@remark,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";

//                    //消息在平台推送物流单消息表中的标识id
//                    id = await conn.QueryFirstAsync<int>(sqlstr, new
//                    {
//                        routeAgreementNo=reqPushRoutPriceMsgDto.msgInfo.routeAgreementNo,
//                        applyNO= reqPushRoutPriceMsgDto.msgInfo.applyNo,
//                        checkState=reqPushRoutPriceMsgDto.msgInfo.status,
//                        time=reqPushRoutPriceMsgDto.msgInfo.time,
//                        remark= reqPushRoutPriceMsgDto.msgInfo.remark
//                    },transaction);

//                    DynamicParameters dp = new DynamicParameters();

//                    dp.Add("@ApplyNo", reqPushRoutPriceMsgDto.msgInfo.applyNo);
//                    dp.Add("@Status", reqPushRoutPriceMsgDto.msgInfo.status);

//                    //审批未通过时,找到申请单号对应的价格表更新的审批状态；审批通过时,不存在则新增,存在则更新
//                    var result= await conn.ExecuteScalarAsync<string>("UN_CheckRoutPrice", dp, transaction, commandType: CommandType.StoredProcedure);
//                    if (result.ToString()=="1")
//                        res.result = new ResMsgDto() { msg = "推送消息成功!" };

//                    transaction.Commit();
//                    conn.Close();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    res.success = "false";
//                    res.resultMessage = "推送消息失败：" + ex.Message;
//                    res.result = new ResMsgDto() { msg = "推送消息失败！" };

//                }
//            }
//            return res;
//        }

//        /// <summary>
//        /// 推送追加费用确认消息
//        /// </summary>
//        /// <param name="reqPushAddExpCofmMsg">确认请求</param>
//        /// <param name="httpContext">请求上下文</param>
//        /// <returns></returns>
//        public async Task<ResModel<ResMsgDto>> LogiticOrderAddExpComf(ReqPushAddExpCofmMsgDto reqPushAddExpCofmMsg, HttpContext httpContext)
//        {
//            ResModel<ResMsgDto> res = new ResModel<ResMsgDto>();
//            res.success = "success";
//            res.resultMessage = "推送消息成功！";

//            string strConn = _unDbConnection.GetDbConnStr();

//            using (IDbConnection conn = DapperHelper.GetOpenConnection(strConn, DbProvider.SqlServer))
//            {
//                IDbTransaction transaction = conn.BeginTransaction();
//                try
//                {
//                    var id = 0;

//                    //记录接口调用
//                    string ipStr = httpContext.Connection.RemoteIpAddress.ToString();
//                    string sqlstr = @" insert into INTERFACE_CALL_LOG(FUNCTION_NAME,FUNCTION_PARAM,Ip,CALL_DATE)
//                            select 'LogiticOrderAddExpComf','ReqPushAddExpCofmMsgDto;HttpContext',@ip,getdate()";
//                    await conn.ExecuteAsync(sqlstr, new { ip=ipStr },transaction);

//                    //记录推送过来的消息
//                    sqlstr = @"insert into T_PUSHMSG (OrderNO,Types,MsgTime,NeedEleContract,EleContractNo,AddDate) 
//select @orderNo,8,@time,@needEleContract,@eleContractNo,getdate(); SELECT CAST(SCOPE_IDENTITY() as int)";

//                    //消息在平台推送物流单消息表中的标识id
//                    id = await conn.QueryFirstAsync<int>(sqlstr, new
//                    {
//                        orderNo=reqPushAddExpCofmMsg.msgInfo.logisticsOrderNo,
//                        time=reqPushAddExpCofmMsg.msgInfo.time,
//                        needEleContract=reqPushAddExpCofmMsg.msgInfo.needEleContract,
//                        eleContractNo=reqPushAddExpCofmMsg.msgInfo.eleContractNo,
//                    },transaction);

//                    //插入记录到消息队列中,待调用物流单查询接口推送确认结果给平台
//                    sqlstr = @"insert into T_MsgQueue(InterfaceId,InterfaceName,ParamId,ProcessState) select '2.11','电子合同查询接口',@id,0";
//                    await conn.ExecuteAsync(sqlstr, new { id = id },transaction);

//                    res.result = new ResMsgDto() { msg = "推送消息成功!" };

//                    transaction.Commit();
//                    conn.Close();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
//                    res.success = "false";
//                    res.resultMessage = "推送消息失败：" + ex.Message;
//                    res.result = new ResMsgDto() { msg = "推送消息失败！" };

//                }
//            }
//            return res;
//        }


    }
}