using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Common.ApiRequest;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 获取库存流水明细
    /// </summary>
    public class GetInventoryFlow : PagingModel
    {
        /// <summary>
        /// 来源单号
        /// </summary>
        public string docentry { get; set; }

        /// <summary>
        /// 物资编码
        /// </summary>
        public string productCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 物资类别
        /// </summary>
        public string productType { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string orderType { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string beginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

    }
}
