using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 盘点单明细
    /// </summary>
    public class ResCheckTaskDt
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Cid { get; set; }

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

        /// <summary>
        /// 是否账实一致
        /// </summary>
        public string isSame { get; set; }

        /// <summary>
        /// 盘点状态
        /// </summary>
        public string checkStatus { get; set; }
    }
}
