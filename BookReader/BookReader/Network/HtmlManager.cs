using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace BookReader.Network
{
    public class HtmlManager
    {
        public static (HttpStatusCode responseCode, string response) GetHtml(string url, Dictionary<string,string> headers)
        {
            HttpItem hi = new HttpItem();
            hi.URL = url;
            Type hiType = hi.GetType();
            foreach (var header in headers)
            {
                var propertyInfo = hiType.GetProperty(header.Key, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(hi, header.Value);
                }
                else
                {
                    hi.Header.Add(header.Key, header.Value);
                }
            }
            HttpResult result = new HttpHelper().GetHtml(hi);
            return (result.StatusCode, result.Html);
        }
    }
}