using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TmsOpenForWoMall.Dto.Request.Union
{
    /// <summary>
    /// 物流查询接口输入类实体
    /// </summary>
    public class ReqOrderInfoDto
    {
        /// <summary>
        /// 授权时获取的access token
        /// </summary>
        [Required(ErrorMessage = "Access token不能为空")]
        public string token { get; set; }

        /// <summary>
        /// queryLogisticsOrderInfo
        /// </summary>
        [Required]
        public string method { get; set; }

        /// <summary>
        /// 物流订单号
        /// </summary>
        [Required]
        public string logisticsOrderNo { get; set; }


    }
}
