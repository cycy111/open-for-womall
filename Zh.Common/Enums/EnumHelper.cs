using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Zh.Common.Enums
{
    /// <summary>
    /// 枚举类型的帮助类。
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举类型的描述。
        /// </summary>
        /// <param name="en">枚举变量</param>
        /// <returns>枚举的描述字符串.</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();   //获取类型  
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员  
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性  

                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述  
                }
            }
            return en.ToString();
        }
    }
}
