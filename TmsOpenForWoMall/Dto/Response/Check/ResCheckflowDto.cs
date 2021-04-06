using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    public class ResCheckflowDto
    {
        /// <summary>
        /// 操作
        /// </summary>
        public string Operate { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Sugest { get; set; }
    }
}
