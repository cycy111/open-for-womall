using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using ProvideForUnionAPI.Dto.Request;
using ProvideForUnionAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsOpenForWoMall.Dal.CheckDal;
using TmsOpenForWoMall.Domain.IServices;
using TmsOpenForWoMall.Dto.Request.Union;
using TmsOpenForWoMall.Dto.Response.Union;
using TmsOpenForWoMall.Infrastructure.DbCommon;
using WmsReport.Infrastructure.Config;
using Zh.Common.ApiRespose;

namespace TmsOpenForWoMall.Domain.Services
{
    /// <summary>
    /// 联通商场数据实现方法
    /// </summary>
    public class UnionService:IUnionService
    {
        private UnionDal _unionDal;

       
        /// <summary>
        /// 创建构造函数
        /// </summary>
        /// <param name="dbConnsAccessor"></param>
        /// <param name="_unDbConnection"></param>
        public UnionService(IOptionsMonitor<DbConnections> dbConnsAccessor,
            IUnionDbConnection _unDbConnection)
        {
            _unionDal = new UnionDal(_unDbConnection);
        }
        
        /// <summary>
        /// 记录调用获取Access token接口
        /// </summary>
        /// <param name="reqTokenDto"></param>
        /// <param name="httpContext"></param>
        public async Task<ResModel<ResTokenDto>> GetToken(ReqTokenDto reqTokenDto, HttpContext httpContext)
        {
            var res = await _unionDal.GetToken(reqTokenDto,httpContext);
            return res;
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="reqPushMsgDto">消息实体</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<ResModel<JObject>> getOrderPushMsg(ReqPushMsgDto reqPushMsgDto, HttpContext httpContext)
        {
            return await _unionDal.getOrderPushMsg(reqPushMsgDto, httpContext);

        }
        /// <summary>
        /// 物流单号
        /// </summary>
        /// <param name="orderNo">物流单号</param>
        /// <param name="context">请求上下文 </param>
        /// <returns></returns>
        public async Task<ResModel<JObject>> GetLogiticOrderByNo(string orderNo, HttpContext context)
        {
            return await _unionDal.GetLogiticOrderByNo(orderNo,context);
        }

        /// <summary>
        /// 推送新增物流订单消息 
        /// </summary>
        /// <param name="addMsg">消息实体</param>
        /// <param name="context">请求上下文 </param>
        /// <returns></returns>
        public async Task<ResModel<JObject>> AddLogiticOrder(AddMsg addMsg, HttpContext context)
        {
            return await _unionDal.AddLogiticOrder(addMsg,context);
        }

        ///// <summary>
        ///// 推送物流订单确认消息
        ///// </summary>
        ///// <param name="reqPushCfmMsgDto"></param>
        ///// <param name="httpContext"></param>
        ///// <returns></returns>
        //public async Task<ResModel<ResMsgDto>> LogiticOrderConfirm(ReqPushCfmMsgDto reqPushCfmMsgDto, HttpContext httpContext)
        //{
        //    return await _unionDal.LogiticOrderConfirm(reqPushCfmMsgDto,httpContext);
        //}

        ///// <summary>
        ///// 推送物流订单追加费用消息
        ///// </summary>
        ///// <param name="reqPushAddExpMsgDto">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //public async Task<ResModel<ResMsgDto>> LogiticOrderAddExpense(ReqPushAddExpMsgDto reqPushAddExpMsgDto, HttpContext httpContext)
        //{
        //    return await _unionDal.LogiticOrderAddExpense(reqPushAddExpMsgDto, httpContext);
        //}

        ///// <summary>
        ///// 推送妥投驳回消息
        ///// </summary>
        ///// <param name="reqPushDelivRejMsgDto">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //public async Task<ResModel<ResMsgDto>> LogiticOrderDelivRej(ReqPushDelivRejMsgDto reqPushDelivRejMsgDto, HttpContext httpContext)
        //{
        //    return await _unionDal.LogiticOrderDelivRej(reqPushDelivRejMsgDto, httpContext);

        //}
        ///// <summary>
        ///// 推送线路价格更新审批结果
        ///// </summary>
        ///// <param name="reqPushRoutPriceMsgDto">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //public async Task<ResModel<ResMsgDto>> RoutPriceUpdate(ReqPushRoutPriceMsgDto reqPushRoutPriceMsgDto, HttpContext httpContext)
        //{
        //    return await _unionDal.RoutPriceUpdate(reqPushRoutPriceMsgDto, httpContext);

        //}
        ///// <summary>
        ///// 推送追加费用确认消息
        ///// </summary>
        ///// <param name="reqPushAddExpCofmMsg">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //public async Task<ResModel<ResMsgDto>> LogiticOrderAddExpComf(ReqPushAddExpCofmMsgDto reqPushAddExpCofmMsg, HttpContext httpContext)
        //{
        //    return await _unionDal.LogiticOrderAddExpComf(reqPushAddExpCofmMsg,httpContext);
        //}
    }
}
