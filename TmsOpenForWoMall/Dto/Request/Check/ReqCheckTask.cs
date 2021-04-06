using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 新增抽盘盘点单
    /// </summary>
    public class ReqCheckTask
    {      
        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 物资数量
        /// </summary>
        public int productNum { get; set; }

        /// <summary>
        /// 盘点生成类型 1.物资 2.箱号
        /// </summary>
        public int addType { get; set; }

    }
}
