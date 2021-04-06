using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 申请单
    /// </summary>
    public class ResApplyDocDto
    {
       public List<ResApplyDocTitle> ResApplyDocTitleDtos;
       public List<ResApplyDocDetl> ResApplyDocDetlDtos;

    }

    /// <summary>
    /// 申请单表头
    /// </summary>
    public class ResApplyDocTitle
    {
        //DocEntry, DocDate, CreateDate, UserSign, DocStatus, OwnerCode, DeptCode, TransType, UpdateDate, 
        //UpdateUser, Remark, CheckUser, CheckDate, OutStatus, AssCode, DliveryDate, DliveryUser, DliveryDW, DliveryPhone, 
        //        OutType, OutOrz, OutWho, RecAddress, DliveryType, DmadDept, DmadKS, DmadPhone, DmadUser, UseDept, UseKS, 
        //        UsePhone, UseUser, PactCode, PactName, ProjectCode, ProjectName, ProjectStatus, ProjectUser, EnComCode, 
        //        EnComName, TotalVolume, Supplier, ProductType, RecPhone, Driver, Receiver, Mileage, SettlePrice, ApplyID, 
        //        TotalWeight, Attachment, FstCost, SalePrice, IsSendMessage, IsPDA, IsBoxRec, OutBox, LedCheckStatus
 

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string RecPhone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string DocEntry { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public string DocDate { get; set; }

        /// <summary>
        /// 出库类型
        /// </summary>
        public string OutType { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string ApplyID { get; set; }

        /// <summary>
        /// 移动出库单号
        /// </summary>
        public string AssCode { get; set; }

        /// <summary>
        /// 发料组织
        /// </summary>
        public string OutOrz { get; set; }

        /// <summary>
        /// 物资类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public string DliveryType { get; set; }

        /// <summary>
        /// 需求部门
        /// </summary>
        public string DmadDept { get; set; }

        /// <summary>
        /// 预约提货时间
        /// </summary>
        public string DliveryDate { get; set; }

        /// <summary>
        /// 领用项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 领用项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 提货人
        /// </summary>
        public string DliveryUser { get; set; }

        /// <summary>
        /// 提货人电话
        /// </summary>
        public string DliveryPhone { get; set; }

        /// <summary>
        /// 施工单位编号
        /// </summary>
        public string EnComCode { get; set; }

        /// <summary>
        /// 施工单位名称
        /// </summary>
        public string EnComName { get; set; }

        /// <summary>
        /// 出库状态
        /// </summary>
        public string OutStatus { get; set; }

        /// <summary>
        /// 领导审核状态
        /// </summary>
        public string LedCheckStatus { get; set; }

        /// <summary>
        /// 发送短信
        /// </summary>
        public string IsSendMessage { get; set; }

        /// <summary>
        /// 是否走PDA
        /// </summary>
        public string IsPDA { get; set; }
    }

    /// <summary>
    /// 申请单明细
    /// </summary>
    public class ResApplyDocDetl
    {
        //DocEntry, LineNum, PartQty, DliveryQty, ProductCode, ProductName, ProductType, ProductUnit, PackCode, 
        //        PackingCode, Price, Amount, Remark, Account, Supplier, SpcModel, PurchaseCode, ProProperty, CGDID, MISOrderID, 
        //        Weight, Volume, ProjectCode, ProjectName, ProSeqno, LedgerName, KcDocEntry, KcLineNum, UndeQty, Location, 
        //        WhCode, KcRemark, KcQty, MappingCode, MappingName, PackagingType, SupName, ProductTypeName, TotalWeight, 
        //        ZsDate, MaterialSname, OutBox, DateBcTm

        /// <summary>
        /// 可用库存
        /// </summary>
        public string KcQty { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public string CGDID { get; set; }

        /// <summary>
        /// 追溯时间
        /// </summary>
        public string DateBcTm { get; set; }

        /// <summary>
        /// 包装类型
        /// </summary>
        public string PackagingType { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string PackCode { get; set; }

        /// <summary>
        /// 装箱单号
        /// </summary>
        public string PackingCode { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public string PartQty { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 物资编号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 物资类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 储位
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 映射编码
        /// </summary>
        public string MappingCode { get; set; }

        /// <summary>
        /// 映射名称
        /// </summary>
        public string MappingName { get; set; }

        /// <summary>
        /// 物资简称
        /// </summary>
        public string MaterialSname { get; set; }

        /// <summary>
        /// 库存单位
        /// </summary>
        public string ProductUnit { get; set; }

        /// <summary>
        /// 库存项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 库存项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 物资批次
        /// </summary>
        public string ProSeqno { get; set; }

        /// <summary>
        /// 关联单号
        /// </summary>
        public string PurchaseCode { get; set; }

        /// <summary>
        /// 出库备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string SpcModel { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupName { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 重量(t)
        /// </summary>
        public string TotalWeight { get; set; }

        /// <summary>
        /// 本次领用数量
        /// </summary>
        public string UndeQty { get; set; }

        /// <summary>
        /// 标准体积（m³）
        /// </summary>
        public string Volume { get; set; }

        /// <summary>
        /// 标准重量(kg)
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string WhCode { get; set; }

        /// <summary>
        /// 闲置天数
        /// </summary>
        public string ZsDate { get; set; }

        /// <summary>
        /// 对应台账名称
        /// </summary>
        public string LedgerName { get; set; }
    }
}
