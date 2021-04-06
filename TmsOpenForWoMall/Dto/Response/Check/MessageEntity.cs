using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 短信实体类
    /// </summary>
    public class MessageEntity
    {

        /// <summary>
        /// 短信接收人号码
        /// </summary>
        public string phoneNumbers { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string signName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 模板code
        /// </summary>
        public string templateCode { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<object, object> templateParam { get; set; }

    }
}
