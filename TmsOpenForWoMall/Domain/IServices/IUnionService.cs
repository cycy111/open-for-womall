using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using ProvideForUnionAPI.Dto.Request;
using ProvideForUnionAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmsOpenForWoMall.Dto.Request.Union;
using TmsOpenForWoMall.Dto.Response.Union;
using Zh.Common.ApiRespose;

namespace TmsOpenForWoMall.Domain.IServices
{
    /// <summary>
    /// 联通商场推送消息接口
    /// </summary>
    public interface IUnionService
    {
        /// <summary>
        /// 记录调用获取Access token接口
        /// </summary>
        /// <param name="reqTokenDto"></param>
        /// <param name="httpContext"></param>
        Task<ResModel<ResTokenDto>> GetToken(ReqTokenDto reqTokenDto, HttpContext httpContext);

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="reqPushMsgDto">消息实体</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<ResModel<JObject>> getOrderPushMsg(ReqPushMsgDto reqPushMsgDto, HttpContext httpContext);
        /// <summary>
        /// 联通商场数据实现方法
        /// </summary>
        /// <param name="orderNo">物流单号</param>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        Task<ResModel<JObject>> GetLogiticOrderByNo(string orderNo, HttpContext context);

        /// <summary>
        /// 推送新增物流订单消息 
        /// </summary>
        /// <param name="addMsg">消息实体</param>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        Task<ResModel<JObject>> AddLogiticOrder(AddMsg addMsg, HttpContext context);

        ///// <summary>
        ///// 推送物流订单确认消息
        ///// </summary>
        ///// <param name="reqPushCfmMsgDto"></param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //Task<ResModel<ResMsgDto>> LogiticOrderConfirm(ReqPushCfmMsgDto reqPushCfmMsgDto, HttpContext httpContext);

        ///// <summary>
        ///// 推送物流订单追加费用消息
        ///// </summary>
        ///// <param name="reqPushAddExpMsgDto">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //Task<ResModel<ResMsgDto>> LogiticOrderAddExpense(ReqPushAddExpMsgDto reqPushAddExpMsgDto, HttpContext httpContext);

        ///// <summary>
        ///// 推送妥投驳回消息
        ///// </summary>
        ///// <param name="reqPushDelivRejMsgDto">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //Task<ResModel<ResMsgDto>> LogiticOrderDelivRej(ReqPushDelivRejMsgDto reqPushDelivRejMsgDto, HttpContext httpContext);

        ///// <summary>
        ///// 推送线路价格更新审批结果
        ///// </summary>
        ///// <param name="reqPushRoutPriceMsgDto">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //Task<ResModel<ResMsgDto>> RoutPriceUpdate(ReqPushRoutPriceMsgDto reqPushRoutPriceMsgDto, HttpContext httpContext);
        ///// <summary>
        ///// 推送追加费用确认消息
        ///// </summary>
        ///// <param name="reqPushAddExpCofmMsg">确认请求</param>
        ///// <param name="httpContext">请求上下文</param>
        ///// <returns></returns>
        //Task<ResModel<ResMsgDto>> LogiticOrderAddExpComf(ReqPushAddExpCofmMsgDto reqPushAddExpCofmMsg, HttpContext httpContext);
    }
}
