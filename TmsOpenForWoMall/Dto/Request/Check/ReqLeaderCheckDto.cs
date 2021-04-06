using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Request.Check
{
    public class ReqLeaderCheckDto
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 系统单号
        /// </summary>
        public string outCode { get; set; }

        /// <summary>
        /// 审批类型(1:同意; 0:驳回)
        /// </summary>
        public string actionType { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string CheckSugest { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string checkerUser { get; set; }
    }
}
