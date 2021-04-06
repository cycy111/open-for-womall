using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ProvideForUnionAPI.Dto.Request;
using ProvideForUnionAPI.Model;
using System;
using System.Threading.Tasks;
using TmsOpenForWoMall.Domain.IServices;
using TmsOpenForWoMall.Dto.Request.Union;
using WmsReport.Infrastructure.Enum;
using Zh.Common.ApiRespose;
using Zh.Common.Cryptography;
using Zh.Common.Enums;
using Zh.Common.Jwt;
using Newtonsoft.Json;

namespace WmsReport.Controllers
{
    /// <summary>
    ///向联通提供的接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UnionController : Controller
    {
        // GET: api/OperatingReport
        private IConfiguration _configuration;
        private ILogger<UnionController> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        private IUnionService _iunion;

        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger">日志</param>
        /// <param name="httpContextAccessor">请求上下文</param>
        /// <param name="iunion">盘点接口</param>
        /// <param name="hostingEnvironment">根目录类</param>
        public UnionController(IConfiguration configuration,
            ILogger<UnionController> logger,
            IHttpContextAccessor httpContextAccessor,
            IUnionService iunion,
            IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _iunion = iunion;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 获取Access Token
        /// </summary>
        /// <param name="reqToken"></param>
        /// <returns></returns>
        [Route("auth")]
        [AllowAnonymous]
        [HttpPost]
        [EnableCors("any")]
        public async Task<IActionResult> GetToken([FromForm]ReqTokenDto reqToken)
        {
            ResModel<ResTokenDto> res = new ResModel<ResTokenDto>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            HttpContext context = _httpContextAccessor.HttpContext;

            IActionResult response = Unauthorized();
            //var user = AuthenticateUser(login);
           

            ResTokenDto token = new ResTokenDto();
            try
            {
                //验证客户端应用唯一编号，调用时采用MD5加密传输
                
                //var crpid = DesHelper.Decrypt(reqToken.corp_id);
                if (_configuration["Jwt:client_id"] != reqToken.client_id || _configuration["Jwt:client_secret"] != reqToken.client_secret 
                    || _configuration["Jwt:corp_id"]!= reqToken.corp_id)
                {
                    res.success = "false";
                    res.resultMessage = "无权限访问！";
                    
                }
                else
                {
                    if (reqToken.response_type == "token")
                    {
                        //记录调用获取Access token接口
                        res = await _iunion.GetToken(reqToken, context);
                        if (res.success ==  "true")
                        {
                            var tokenString = GenerateJSONWebToken(reqToken);
                            token = new ResTokenDto { access_token = tokenString, time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), expire_in = DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss"), state = reqToken.state };
                            res.success = "true";
                            res.result = token;
                        }    
                    }
                    else
                    {
                        res.success = "false";
                        res.resultMessage = "无权限访问！";

                    }
                }
                
                response = Ok(res);

            }
            catch (Exception ex)
            {
                return Ok(res.GetRes("false", ex.Message, null));
            }
            return response;
        }

        ///// <summary>
        ///// 平台根据物流单查询当前物流单的物流配送信息
        ///// </summary>
        ///// <param name="reqOrderInfo">物流单 </param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("queryLogisticsOrderInfo")]
        //[EnableCors("any")]
        ////[Authorize]
        //public async Task<ActionResult<ResModel<ResOrderInfoDto>>> queryLogisticsOrderInfo([FromBody]ReqOrderInfoDto reqOrderInfo )
        //{
        //    var res = new ResModel<ResOrderInfoDto>();

        //    //未授权
        //    JObject token = CheckAuthorize(reqOrderInfo.token);
        //    if (token == null) //未授权 24小时过期，重新获取
        //    {
        //        res.success = "false";
        //        res.resultMessage = "无权限访问";
        //        return res;
        //    };

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    HttpContext context =_httpContextAccessor.HttpContext;
        //    string logiticOrderNo = reqOrderInfo.logisticsOrderNo;
        //    if (reqOrderInfo.method== "queryLogisticsOrderInfo")
        //    {
        //        try
        //        {
        //            res =await _iunion.GetLogiticOrderByNo(logiticOrderNo,context);
        //            return res;
        //        }
        //        catch (Exception ex)
        //        {
        //            res.success = "false";
        //            return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
        //        }
        //    }
        //    else
        //    {
        //        res.success = "false";
        //        res.resultMessage = "请求method错误";
        //    }
        //    return res;
        //}
        /// <summary>
        /// 业务 
        /// </summary>
        /// <param name="pushMsgDto"></param>
        /// <returns></returns>[HttpPost]
        [HttpPost]
        [Route("getOrderPushMsg")]
        //[Consumes("application/x-www-form-urlencoded")]
        [EnableCors("any")]
        //[Authorize]
        //[ValidateModel]
        public async Task<ActionResult<ResModel<JObject>>> getOrderPushMsg([FromForm]ReqPushMsgDto pushMsgDto)
        {
            //ReqPushMsgDto pushmsg = JsonConvert.DeserializeObject<ReqPushMsgDto>(pushMsgDto);
            //ResModel<ResMsgDto> res = new ResModel<ResMsgDto>();
            ResModel<JObject> res = new ResModel<JObject>();

            //未授权
            //未授权
            JObject token = CheckAuthorize(pushMsgDto.token?.ToString());
            if (token == null) //未授权 24小时过期，重新获取
            {
                res.success = "false";
                res.resultMessage = "无权限访问";
                return res;
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            HttpContext context = _httpContextAccessor.HttpContext;

            if (pushMsgDto.method?.ToString() == "getOrderPushMsg") 
            {

                //string msgInfo = JObject.FromObject(pushMsgDto.msgInfo).ToString();
               
                if (pushMsgDto.type?.ToString() == "1")
                {
                    try
                    {

                        var obj = JsonConvert.DeserializeObject<AddMsg>(pushMsgDto.msgInfo);

                        res = await _iunion.AddLogiticOrder(obj, context);
                    }
                    catch (Exception ex)
                    {
                        return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
                    }
                }
                else if (pushMsgDto.type == "2" || pushMsgDto.type == "3" || pushMsgDto.type == "6" || pushMsgDto.type == "7" || pushMsgDto.type == "8")
                {
                    try
                    {
                        res = await _iunion.getOrderPushMsg(pushMsgDto, context);
                        return res;
                    }
                    catch (Exception ex)
                    {
                        return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
                    }
                }
                else
                {
                    res.success = "false";
                    res.resultMessage = "请求method、推送消息类型或者消息实体错误";
                }
            }
            else if(pushMsgDto.method?.ToString() == "queryLogisticsOrderInfo")
            {
                try
                {
                    
                    res = await _iunion.GetLogiticOrderByNo(pushMsgDto.logisticsOrderNo?.ToString(), context);
                    return res;
                }
                catch (Exception ex)
                {
                    res.success = "false";
                    return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
                }
            }
            else
            {
                res.success = "false";
                res.resultMessage = "请求method错误";
            }



            return res;
        }

        ///// <summary>
        ///// 推送物流订单确认消息
        ///// </summary>
        ///// <param name="reqPushCfmMsgDto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("OrderComfirm")]
        ////[Authorize]
        //public async Task<ActionResult<ResModel<ResMsgDto>>> LogiticOrderConfirm([FromBody]ReqPushCfmMsgDto reqPushCfmMsgDto)
        //{
        //    var res = new ResModel<ResMsgDto>();

        //    //未授权
        //    JObject token = CheckAuthorize(reqPushCfmMsgDto.token);
        //    if (token == null) //未授权 24小时过期，重新获取
        //    {
        //        res.success = "false";
        //        res.resultMessage = "无权限访问";
        //        return res;
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    HttpContext httpContext = _httpContextAccessor.HttpContext;
            
        //    //检查新增物流消息的特殊标识
        //    if (reqPushCfmMsgDto.method == "getOrderPushMsg" && reqPushCfmMsgDto.type=="2")
        //    {
        //        try
        //        {
        //            res = await _iunion.LogiticOrderConfirm(reqPushCfmMsgDto, httpContext);
        //            return res;
        //        }
        //        catch (Exception ex)
        //        {
        //            return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
        //        }
        //    }
        //    else
        //    {
        //        res.success = "false";
        //        res.resultMessage = "请求method错误";
        //    }
        //    return res;
        //}

        ///// <summary>
        ///// 推送物流订单追加费用消息
        ///// </summary>
        ///// <param name="reqPushAddExpMsgDto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("AddExp")]
        ////[Authorize]
        //public async Task<ActionResult<ResModel<ResMsgDto>>> LogiticOrderAddExpense([FromBody]ReqPushAddExpMsgDto reqPushAddExpMsgDto)
        //{
        //    var res = new ResModel<ResMsgDto>();

        //    //未授权
        //    JObject token = CheckAuthorize(reqPushAddExpMsgDto.token);
        //    if (token == null) //未授权 24小时过期，重新获取
        //    {
        //        res.success = "false";
        //        res.resultMessage = "无权限访问";
        //        return res;
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    HttpContext httpContext = _httpContextAccessor.HttpContext;

        //    //检查物流订单追加费用消息的特殊标识
        //    if (reqPushAddExpMsgDto.method == "getOrderPushMsg" && reqPushAddExpMsgDto.type == "3")
        //    {
        //        try
        //        {
        //            res = await _iunion.LogiticOrderAddExpense(reqPushAddExpMsgDto, httpContext);
        //            return res;
        //        }
        //        catch (Exception ex)
        //        {
        //            return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
        //        }
        //    }
        //    else
        //    {
        //        res.success = "false";
        //        res.resultMessage = "请求method错误";
        //    }
        //    return res;
        //}

        ///// <summary>
        /////  推送妥投驳回消息
        ///// </summary>
        ///// <param name="reqPushDelivRejMsgDto">确认请求</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("DeliverRej")]
        ////[Authorize]
        //public async Task<ActionResult<ResModel<ResMsgDto>>> LogiticOrderDelivRej(ReqPushDelivRejMsgDto reqPushDelivRejMsgDto)
        //{
        //    var res = new ResModel<ResMsgDto>();

        //    //未授权
        //    JObject token = CheckAuthorize(reqPushDelivRejMsgDto.token);
        //    if (token == null) //未授权 24小时过期，重新获取
        //    {
        //        res.success = "false";
        //        res.resultMessage = "无权限访问";
        //        return res;
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    HttpContext httpContext = _httpContextAccessor.HttpContext;

        //    //检查物流订单追加费用消息的特殊标识
        //    if (reqPushDelivRejMsgDto.method == "getOrderPushMsg" && reqPushDelivRejMsgDto.type == "6")
        //    {
        //        try
        //        {
        //            res =await _iunion.LogiticOrderDelivRej(reqPushDelivRejMsgDto, httpContext);
        //            return res;
        //        }
        //        catch (Exception ex)
        //        {
        //            return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
        //        }
        //    }
        //    else
        //    {
        //        res.success = "false";
        //        res.resultMessage = "请求method错误";
        //    }
        //    return res;
        //}

        ///// <summary>
        ///// 推送线路价格更新审批结果
        ///// </summary>
        ///// <param name="reqPushRoutPriceMsgDto">确认请求</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("PriceUpdate")]
        ////[Authorize]
        //public async Task<ActionResult<ResModel<ResMsgDto>>> RoutPriceUpdate(ReqPushRoutPriceMsgDto reqPushRoutPriceMsgDto)
        //{
        //    var res = new ResModel<ResMsgDto>();

        //    //未授权
        //    JObject token = CheckAuthorize(reqPushRoutPriceMsgDto.token);
        //    if (token == null) //未授权 24小时过期，重新获取
        //    {
        //        res.success = "false";
        //        res.resultMessage = "无权限访问";
        //        return res;
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    HttpContext httpContext = _httpContextAccessor.HttpContext;

        //    //检查物流订单追加费用消息的特殊标识
        //    if (reqPushRoutPriceMsgDto.method == "getOrderPushMsg" && reqPushRoutPriceMsgDto.type == "7")
        //    {
        //        try
        //        {
        //            res = await _iunion.RoutPriceUpdate(reqPushRoutPriceMsgDto, httpContext);
        //            return res;
        //        }
        //        catch (Exception ex)
        //        {
        //            return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
        //        }
        //    }
        //    else
        //    {
        //        res.success = "false";
        //        res.resultMessage = "请求method错误";
        //    }
        //    return res;
        //}
        ///// <summary>
        ///// 推送追加费用确认消息
        ///// </summary>
        ///// <param name="reqPushAddExpCofmMsg">确认请求</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("AddExpComfm")]
        ////[Authorize]
        //public async Task<ActionResult<ResModel<ResMsgDto>>> LogiticOrderAddExpComf(ReqPushAddExpCofmMsgDto reqPushAddExpCofmMsg)
        //{
        //    var res = new ResModel<ResMsgDto>();

        //    //未授权
        //    JObject token = CheckAuthorize(reqPushAddExpCofmMsg.token);
        //    if (token == null) //未授权 24小时过期，重新获取
        //    {
        //        res.success = "false";
        //        res.resultMessage = "无权限访问";
        //        return res;
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    HttpContext httpContext = _httpContextAccessor.HttpContext;

        //    //检查物流订单追加费用消息的特殊标识
        //    if (reqPushAddExpCofmMsg.method == "getOrderPushMsg" && reqPushAddExpCofmMsg.type == "8")
        //    {
        //        try
        //        {
        //            res = await _iunion.LogiticOrderAddExpComf(reqPushAddExpCofmMsg, httpContext);
        //            return res;
        //        }
        //        catch (Exception ex)
        //        {
        //            return res.GetRes(Convert.ToBoolean((int)ErrorEnum.SystemException).ToString(), EnumHelper.GetDescription(ErrorEnum.SystemException));
        //        }
        //    }
        //    else
        //    {
        //        res.success = "false";
        //        res.resultMessage = "请求method错误";
        //    }
        //    return res;
        //}
        private string GenerateJSONWebToken(ReqTokenDto reqToken)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:corp_id"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secret = _configuration["Jwt:client_secret"].ToString();

            //var claims = new[] {
            //new Claim(JwtRegisteredClaimNames.Jti, userInfo.user),
            //new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
            //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
            // new Claim(JwtRegisteredClaimNames.Jti, await Guid.NewGuid().ToString())
            // };

            //var token = new JwtSecurityToken(_configuration["Jwt:client_id"],
            //    _configuration["Jwt:client_secret"],
            //    null,
            //    expires: DateTime.Now.AddHours(24),
            //    signingCredentials: credentials);

            var token= new JwtBuilder()
              .WithAlgorithm(new HMACSHA256Algorithm())
              .WithSecret(secret)
              .Build();

            return token;
        }

        /// <summary>
        /// 验证权限 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private JObject CheckAuthorize(string token)
        {
            //权限认证
           // string token = _httpContextAccessor.HttpContext.Request.Headers["token"];
            JObject tokenJson = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    string secret = _configuration["Jwt:client_secret"].ToString();
                    tokenJson = JwtHelper.GetJwtJson(token, secret);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                    tokenJson = null;
                }

            }
            if (tokenJson == null)
            {
                //未授权
                return null;
            }
            return tokenJson;
        }
    }
    
}
