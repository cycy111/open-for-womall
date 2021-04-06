using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProvideForUnionAPI.Dto.Request
{
    /// <summary>
    /// 获取Access token请求类
    /// </summary>
    public class ReqTokenDto
    {
        /// <summary>
        /// 表示授权类型，此处的值固定为"token"
        /// </summary>
        [Required(ErrorMessage = "授权类型不能为空")]
        public string response_type { get; set; }

        /// <summary>
        /// 表示客户端的ID (由物流商提供)
        /// </summary>
        [Required(ErrorMessage = "客户端的ID不能为空")]
        public string client_id { get; set; }

        /// <summary>
        /// 表示客户端secret (由物流商提供)
        /// </summary>
        [Required(ErrorMessage = "客户端secret不能为空")]
        public string client_secret { get; set; }

        /// <summary>
        /// 表示客户端应用唯一编号（由物流商提供，调用时采用MD5加密传输）
        /// </summary>
        [Required(ErrorMessage = "客户端应用唯一编号不能为空")]
        public string corp_id { get; set; }

        /// <summary>
        /// 表示客户端的当前状态
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 表示权限范围
        /// </summary>
        public string scope { get; set; }
    }
}
