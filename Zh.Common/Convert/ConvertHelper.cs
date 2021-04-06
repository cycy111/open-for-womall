using System;
using System.Collections.Generic;
using System.Text;

namespace Zh.Common.Convert
{
    public class ConvertHelper
    {
        /// <summary>
        /// 字符串转换为int32
        /// </summary>
        /// <param name="input"></param>
        /// <param name="valueIfError"></param>
        /// <returns></returns>
        public static int TryToInt32(string input,int valueIfError = -1)
        {
            int result = valueIfError;
            try
            {
                if (!Int32.TryParse(input, out result))
                {
                   
                    result = valueIfError;
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        /// <summary>
        /// 字符串转换为int32
        /// </summary>
        /// <param name="input"></param>
        /// <param name="valueIfError"></param>
        /// <returns></returns>
        public static int TryToInt32(object input, int valueIfError = -1)
        {
            return TryToInt32((input ?? "").ToString(), valueIfError);
        }

        /// <summary>
        /// 字符串转换为int64
        /// </summary>
        /// <param name="input"></param>
        /// <param name="valueIfError"></param>
        /// <returns></returns>
        public static long TryToInt64(string input, long valueIfError = -1)
        {
            long result = valueIfError;
            try
            {
                if (!Int64.TryParse(input, out result))
                {
                    result = valueIfError;
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        /// <summary>
        /// 字符串转换为int64
        /// </summary>
        /// <param name="input"></param>
        /// <param name="valueIfError"></param>
        /// <returns></returns>
        public static long TryToInt64(object input, long valueIfError = -1)
        {
            return TryToInt64((input ?? "").ToString(), valueIfError);
        }

        public static double TryToDouble(string input,double valueIfError = -1)
        {
            double result = valueIfError;
            try
            {
                if (!double.TryParse(input, out result))
                {
                    result = valueIfError;
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static double TryToDouble(object input, double valueIfError = -1)
        {
            return TryToDouble((input ?? "").ToString(), valueIfError);
        }


        public static DateTime? TryToDateTime(string input)
        {
            DateTime result = new DateTime();
            try
            {
                result = System.Convert.ToDateTime(input);
                return result;
            }
            catch
            {
                return null;
            }
        }
        public static DateTime? TryToDateTime(object input)
        {
            return TryToDateTime((input ?? "").ToString());
        }
    }
}
