using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmsReport.Dto.Response.Check;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 更新盘点单信息
    /// </summary>
    public class UpCheckList
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string checkNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 盘点明细
        /// </summary>
        public List<ResCheckTaskDt> dt { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string userId { get; set; }
    }
}
