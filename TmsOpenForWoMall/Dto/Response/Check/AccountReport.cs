using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 对账报表
    /// </summary>
    public class AccountReport
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string accountNo { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string creator { get; set; }

    }
}
