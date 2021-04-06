using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TmsOpenForWoMall.Dto.Request.Union
{
    /// <summary>
    /// 推送新增物流订单消息请求类
    /// </summary>
    public class ReqPushMsgDto
    {
        /// <summary>
        /// 授权时获取的access token
        /// </summary>
        [Required]
        public string token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string method { get; set; } 

        /// <summary>
        /// 推送消息类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 物流订单号
        /// </summary>
        public string logisticsOrderNo { get; set; }

        /// <summary>
        /// 消息实体
        /// </summary>
        public string msgInfo  { get; set; }

    }
    /// <summary>
    /// 消息实体(type=1)
    /// </summary>
    public class AddMsg
    {
        /// <summary>
        /// 物流订单号（平台生成）
        /// </summary>
        [Required]
        public string logisticsOrderNo { get; set; }

        /// <summary>
        /// 1-新增物流订单
        /// </summary>
        [Required]
        public int? stype { get; set; }

        /// <summary>
        /// 时间 格式：“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        [Required]
        public DateTime time { get; set; }
    }

    /// <summary>
    /// 确认消息类
    /// </summary>
    public class CfmMsg
    {
        /// <summary>
        /// 物流订单号
        /// </summary>
        [Required]
        public string logisticsOrderNo { get; set; }

        /// <summary>
        /// 消息类型 1-确认 2-取消
        /// </summary>
        [Required]
        public string stype { get; set; }

        /// <summary>
        /// 总价格，stype=1时为必填
        /// </summary>
        public string totalPrice { get; set; }

        /// <summary>
        /// 电子合同编号
        /// </summary>
        public string eleContractNo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime time { get; set; }

        /// <summary>
        /// 是否需要确认取消结果：0否1是，stype=2时必填；
        /// </summary>
        [Required]
        public string needReply { get; set; }
    }

    /// <summary>
    /// 追加费用消息类
    /// </summary>
    public class AddExpMsg
    {
        /// <summary>
        /// 物流订单号
        /// </summary>
        [Required]
        public string logisticsOrderNo { get; set; }

        /// <summary>
        /// 追加费用金额（空跑费、装卸费、送仓费）
        /// </summary>
        [Required]
        public string addedExpense { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime time { get; set; }
    }

    /// <summary>
    /// 妥投驳回消息类
    /// </summary>
    public class DelivRejMsg
    {
        /// <summary>
        /// 物流订单号
        /// </summary>
        [Required]
        public string logisticsOrderNo { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        [Required]
        public string deliveredInfo { get; set; }

        /// <summary>
        /// 驳回人姓名
        /// </summary>
        [Required]
        public string deliveredPerson { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime time { get; set; }
    }

    /// <summary>
    /// 线路价格消息类
    /// </summary>
    public class PriceMsg
    {
        /// <summary>
        /// 物流商招募入围编号
        /// </summary>
        [Required]
        public string routeAgreementNo { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        [Required]
        public string applyNo { get; set; }

        /// <summary>
        /// 审批结果：0未通过1通过
        /// </summary>
        [Required]
        public string status { get; set; }

        /// <summary>
        /// 未通过审批时的原因
        /// </summary>
        [Required]
        public string remark { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime time { get; set; }
    }

    /// <summary>
    /// 追加费用消息类
    /// </summary>
    public class pactMsg
    {
        /// <summary>
        /// 物流订单号
        /// </summary>
        [Required]
        public string logisticsOrderNo { get; set; }

        /// <summary>
        /// 是否需要电子合同
        /// </summary>
        [Required]
        public string needEleContract { get; set; }

        /// <summary>
        /// 电子合同编号
        /// </summary>
        public string eleContractNo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime time { get; set; }
    }
}
