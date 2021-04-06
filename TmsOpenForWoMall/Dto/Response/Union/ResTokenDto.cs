using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvideForUnionAPI.Model
{
    public class ResTokenDto
    {
        /// <summary>
        /// 平台访问授权access_token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public string  time { get; set; }

        /// <summary>
        /// access_token的有效时间   
        /// </summary>
        public string expire_in { get; set; }

        /// <summary>
        /// 如果客户端的请求中包含这个参数，认证服务器的回应也必须一模一样
        /// </summary>
        public string state { get; set; }

    }
}
