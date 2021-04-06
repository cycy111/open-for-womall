using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 盘点报告
    /// </summary>
    public class CheckReport
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string CheckNo { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 盘点类型
        /// </summary>
        public string checkType { get; set; }       

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }

    }
}
