using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BookReader.Parser
{
    public class JsonParser
    {
        public static string GetValue(string json, string jpath)
        {
            var o = JObject.Parse(json);
            var token = o.SelectToken(jpath);
            return token.ToString();
        }

        public static List<string> GetList(string json, string jpath)
        {
            JObject o = JObject.Parse(json);
            var tokens = o.SelectTokens(jpath);
            return tokens.Select(x => x.ToString()).ToList();
        }

        public static List<List<string>> GetList(string json, string loopJsonpath, List<string> valueJsonpaths)
        {
            JObject o = JObject.Parse(json);
            var tokens = o.SelectTokens(loopJsonpath);
            var list = new List<List<string>>();
            foreach (var token in tokens)
            {
                List<string> valueList = new List<string>();
                valueJsonpaths.ForEach(path =>
                {
                    valueList.Add(token.SelectToken(path).ToString());
                });
                list.Add(valueList);
            }
            return list;
        }
    }
}