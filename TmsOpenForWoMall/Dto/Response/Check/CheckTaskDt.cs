using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 盘点明细
    /// </summary>
    public class CheckTaskDt
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string cid { get; set; }

        /// <summary>
        /// 盘点单号
        /// </summary>
        public string checkNo { get; set; }

        /// <summary>
        /// 物资编码
        /// </summary>
        public string productCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 储位
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 账目库存
        /// </summary>
        public string kcQty { get; set; }

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
