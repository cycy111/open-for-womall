using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Request.Check
{
    //紧急出库单
    public class ReqUrgecOutDocsDto
    {
       
        /// <summary>
        /// 审核人
        /// </summary>
        public string checkName { get; set; }
 
        /// <summary>
        /// 申请人
        /// </summary>
        public string applyName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string checkStatus { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public string pageSize { get; set; }

        /// <summary>
        /// 页索引
        /// </summary>
        public string pageIndex { get; set; }

    }
}
