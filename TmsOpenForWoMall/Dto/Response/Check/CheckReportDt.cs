using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckReportDt
    {
        /// <summary>
        /// 物资编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 储位
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 账目库存
        /// </summary>
        public string KcQty { get; set; }

        /// <summary>
        /// 盘点数量
        /// </summary>
        public string checkQty { get; set; }

        /// <summary>
        /// 差异数量
        /// </summary>
        public string difQty { get; set; }
    }
}
