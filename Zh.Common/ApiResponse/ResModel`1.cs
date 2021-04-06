using System;
using System.Collections.Generic;
using System.Text;

namespace Zh.Common.ApiRespose
{
    /// <summary>
    /// api接口响应泛型模型。
    /// </summary>
    /// <typeparam name="TData">属性data的类型。</typeparam>
    public class ResModel<TData> :ResModel where TData:class
    {

        /// <summary>
        /// api接口返回的数据。
        /// </summary>
        public TData result
        {
            get; set; }

        public ResModel() { }

        public ResModel(string success,string message,TData data):base(success, message)
        {
            this.success = success;
            this.resultMessage = message;
            this.result = data;   
        }

        /// <summary>
        /// 根据参数，返回一个全新的Response对象。
        /// </summary>
        /// <param name="resultCode">返回码。</param>
        /// <param name="message">返回信息。</param>
        /// <param name="data">返回的数据。</param>
        /// <returns></returns>
        public virtual ResModel<TData> GetRes(string success,string message,TData data = null)
        {
            ResModel<TData> res = new ResModel<TData>(success, message,data);
            return res;
        }

        
    }
}
