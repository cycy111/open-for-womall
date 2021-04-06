using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 盘点任务
    /// </summary>
    public class CheckTask
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string checkNo { get; set; }

        /// <summary>
        /// 盘点类型
        /// </summary>
        public int addType { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 是否账实一致
        /// </summary>
        public string isSame { get; set; }

        /// <summary>
        /// 盘点状态
        /// </summary>
        public string checkStatus { get; set; }

        /// <summary>
        /// 储位
        /// </summary>
        public string location { get; set; }


        /// <summary>
        /// 创建人名称
        /// </summary>
        public string creatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public string sendDate { get; set; }


    }
}
