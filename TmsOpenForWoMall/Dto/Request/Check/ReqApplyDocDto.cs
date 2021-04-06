using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Request.Check
{
    public class ReqApplyDocDto
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 系统单号
        /// </summary>
        public string outCode { get; set; }
    }
}
