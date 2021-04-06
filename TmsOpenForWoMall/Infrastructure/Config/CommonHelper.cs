﻿using FluentEmail.Core;
using FluentEmail.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.Config
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 同步POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout">请求响应超时时间，单位/s(默认100秒)</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns></returns>
        public static string HttpPost(string url, string postData, Dictionary<string, string> headers = null, string contentType = null, int timeout = 0, Encoding encoding = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (timeout > 0)
                {
                    client.Timeout = new TimeSpan(0, 0, timeout);
                }
                using (HttpContent content = new StringContent(postData ?? "", encoding ?? Encoding.UTF8))
                {
                    if (contentType != null)
                    {
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    }
                    using (HttpResponseMessage responseMessage = client.PostAsync(url, content).Result)
                    {
                        Byte[] resultBytes = responseMessage.Content.ReadAsByteArrayAsync().Result;
                        return Encoding.UTF8.GetString(resultBytes);
                    }
                }
            }
        }

        /// <summary>
        /// 发邮件功能
        /// </summary>
        public static async Task<string> SendMail()
        {
            try
            {   
                //注意： .NET 4.6 才支持               
                //Email.DefaultSender = new SmtpSender();          
                // Using Razor templating package
                Email.DefaultRenderer = new RazorRenderer();
                var template = "Dear @Model.Name, You are totally @Model.Compliment.";
                var email = Email
                    .From("463471132@qq.com")
                    .To("463471132@qq.com")
                    .Subject("woo nuget")
                    .UsingTemplate(template, new { Name = "Luke", Compliment = "Awesome" });

                var res = await email.SendAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

           return "success";
        }
    }
}
