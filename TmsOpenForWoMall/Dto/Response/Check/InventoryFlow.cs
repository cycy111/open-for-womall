using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 库存流水明细
    /// </summary>
    public class InventoryFlow
    {

        /// <summary>
        /// 来源单号
        /// </summary>
        public string DocEntry { get; set; }

        /// <summary>
        /// 物资编码
        /// </summary>
        public string productCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string ProductUnit { get; set; }

        /// <summary>
        /// 单类型
        /// </summary>
        public string orderType { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string SupName { get; set; }

        /// <summary>
        /// 物资类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 库存组织
        /// </summary>
        public string InOrganize { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Qty { get; set; }
         
    }
}
