using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms.Internals;

namespace BookReader.Parser
{
    public class HtmlParser
    {

        private static string GetNodeValue(string html, string xpath)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var root = doc.DocumentNode;
                HtmlNodeNavigator navigator = (HtmlNodeNavigator)root.CreateNavigator();
                var node = navigator.SelectSingleNode(xpath);
                return node.Value;
            }
            catch
            {
                return "";
            }
            
        }

        public static string GetValue(string html, string xpath)
        {
            return GetNodeValue(html, xpath);
        }

        public static List<string> GetList(string html, string xpath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var root = doc.DocumentNode;
            var nodes = root.SelectNodes(xpath);
            return nodes.Select(x => x.InnerText).ToList();
        }

        public static List<List<string>> GetList(string html, string loopXpath, List<string> valueXpaths)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var root = doc.DocumentNode;
            var list = new List<List<string>>();
            root.SelectNodes(loopXpath).ForEach(node =>
            {
                List<string> valueList = new List<string>();
                valueXpaths.ForEach(value => valueList.Add(GetNodeValue(node.InnerHtml, value)));
                list.Add(valueList);
            });
            return list;
        }
    }
}