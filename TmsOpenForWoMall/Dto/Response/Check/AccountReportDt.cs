using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 对账明细
    /// </summary>
    public class AccountReportDt
    {
        /// <summary>
        /// 物资编码
        /// </summary>
        public string productCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string projectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectName { get; set; }
        
        /// <summary>
        /// 仓库数据
        /// </summary>
        public string WMSQty { get; set; }

        /// <summary>
        /// 移动数量
        /// </summary>
        public string YDQty { get; set; }

        /// <summary>
        /// 数量差异
        /// </summary>
        public string difQty { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string supName { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public string cgdid { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string packCode { get; set; }

    }
}
