using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.Enum
{
    /// <summary>
    /// 错误信息枚举，前两位表示模块，后两位标识错误类型。
    /// 1代表成功，负数代表失败，正数代表成功的不同情况。
    /// 前2位：
    /// 10 表示通用
    ///     01 表示储位 02 表示箱号 03 表示托盘 04 表示物资条码 05 表示物资条码和储位 
    ///     06 表示物资
    /// 11 表示PDA
    ///    
    
    /// 后2位：
    /// 00 表示不能为空
    /// 01 表示已存在
    /// 02 表示不存在
    /// 03 表示失败
    /// 04 表示错误
    /// 05 系统故障
    /// 06 表示本身
    /// 07 表示已过期
    /// 08 表示未上架
    /// 最后2位是序号，当错误相同时
    /// <summary>
    /// 枚举信息
    /// </summary>
    public enum ErrorEnum
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        [Description("success")]
        Success = 1,

        /// <summary>
        /// 系统异常
        /// </summary>
        [Description("系统异常，请重试")]
        SystemException = -1000,

        /// <summary>
        /// 描述性异常
        /// </summary>
        [Description("描述性异常")]
        DescriptException = -1001,

        /// <summary>
        /// 用户已过期
        /// </summary>
        [Description("用户已过期")]
        TokenExpired = -401,

        /// <summary>
        /// 参数不能为空
        /// </summary>
        [Description("参数不能为空")]
        ParameterNotEmpty = -1001,

        /// <summary>
        /// 新增失败
        /// </summary>
        [Description("新增失败")]
        SaveFail = -1002,

        /// <summary>
        /// 更新失败
        /// </summary>
        [Description("更新失败")]
        UpdateFail = -1003,

        /// <summary>
        /// 此编号已存在
        /// </summary>
        [Description("此编号已存在")]
        IsExists = -1004,

        /// <summary>
        /// 删除失败
        /// </summary>
        [Description("删除失败")]
        DelFail = -1005,

        /// <summary>
        /// 查询的项目类型为空
        /// </summary>
        [Description("查询的项目类型为空")]
        FindFail = -1006,

        #region 月报
        /// <summary>
        /// 部门对应版本已存在
        /// </summary>
        [Description("部门对应版本已存在")]
        IsExistsVersion = -1007,

        /// <summary>
        /// 请配置月报项目基础数据
        /// </summary>
        [Description("请配置月报项目基础数据")]
        MonReportBaseDateNeed = -1008,

        /// <summary>
        /// 无权限访问
        /// </summary>
        [Description("无权限访问")]
        Unauthorized = -1009,

        /// <summary>
        /// 部门已存在
        /// </summary>
        [Description("部门已存在(或历史数据,请更新部门编码)")]
        IsExistsDepart = -1010,

        /// <summary>
        /// 月报项不能为空
        /// </summary>
        [Description("月报项不能为空")]
        ReportItemNotNull = -2010,
        #endregion

        #region pda枚举
        /// <summary>
        /// 入库单不存在或未审核
        /// </summary>
        [Description("入库单不存在或未审核")]
        NotExistsInCode = -1011,

        /// <summary>
        /// 托盘号不存在
        /// </summary>
        [Description("托盘号不存在")]
        NotTrayNo = -1012,

        /// <summary>
        /// 托盘号已使用
        /// </summary>
        [Description("托盘号已使用")]
        IsUsedTray = -1013,

        /// <summary>
        /// 托盘已存在储位
        /// </summary>
        [Description("托盘")]
        IsExistsLocation = -1015,

        /// <summary>
        /// 托盘未绑定入库单
        /// </summary>
        [Description("托盘未绑定入库单")]
        TrayNoNotBind = -1016,
        /// <summary>
        /// 入库单重复绑定
        /// </summary>
        [Description("入库单重复绑定")]
        ReBindInCode = -1017,

        /// <summary>
        /// 出库单不存在
        /// </summary>
        [Description("出库单不存在或未审核")]
        NotExistsOutCode = -1018,

        /// <summary>
        /// 库存不足
        /// </summary>
        [Description("库存不足")]
        NotInventory = -1019,

        /// <summary>
        /// 此箱号已复核
        /// </summary>
        [Description("箱号已复核")]
        BoxHasChecked = -1020,

        /// <summary>
        /// 没可用库存
        /// </summary>
        [Description("没可用库存")]
        UnderStock = -1021,

        /// <summary>
        /// 此箱号已收货
        /// </summary>
        [Description("此箱号已收货")]
        BoxHasReceived = -1022,

        /// <summary>
        /// 此托盘未收货
        /// </summary>
        [Description("此托盘未收货,不能上架")]
        TrayNotReceived = -1023,

        /// <summary>
        /// 此托盘已上架
        /// </summary>
        [Description("此托盘已上架")]
        TrayIsUp = -1024,

        /// <summary>
        /// 请将箱号先下架
        /// </summary>
        [Description("请将箱号先下架")]
        BoxNotDown = -1025,

        /// <summary>
        /// 箱号不存在
        /// </summary>
        [Description("箱号不存在")]
        BoxNotExists = -1026,

        /// <summary>
        /// 请将物资编号先下架
        /// </summary>
        [Description("请将物资编号先下架")]
        ProductNotDown = -1027,

        /// <summary>
        /// 箱号不存在库存中
        /// </summary>
        [Description("箱号不存在库存中")]
        BoxNotExistInventory = -1028,

















        /// <summary>
        /// 储位号不存在
        /// </summary>
        [Description("储位号不存在")]
        LocationNotExist = -100102,

        /// <summary>
        /// 储位号在库存中不存在
        /// </summary>
        [Description("储位号在库存中不存在")]
        LocationNotExistInventory = -100103,

        /// <summary>
        /// 箱号不存在
        /// </summary>
        [Description("箱号不存在")]
        BoxNoNotExist = -100202,

        /// <summary>
        /// 托盘号不存在
        /// </summary>
        [Description("托盘号不存在")]
        TrayNoNotExist = -100302,

        /// <summary>
        /// 托盘号未上架
        /// </summary>
        [Description("托盘号未上架")]
        TrayNoNotUp = -100308,

        /// <summary>
        /// 物资条码不存在
        /// </summary>
        [Description("物资条码不存在")]
        MaterialBarCodeNotExist = -100402,

        /// <summary>
        /// 此储位不存在该物资
        /// </summary>
        [Description("此储位不存在该物资")]
        MaterialNotInTheLocation = -100502,

        /// <summary>
        /// 物资编码不存在
        /// </summary>
        [Description("物资编码不存在")]
        MaterialCodeNotExist = -100602,

        /// <summary>
        /// 物资条码已被绑定
        /// </summary>
        [Description("物资条码已被绑定")]
        MaterialBarCodeIsBinded = -100401,

        #endregion
    }
}
