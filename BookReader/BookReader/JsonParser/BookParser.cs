using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Internals;

namespace BookReader.JsonParser
{
    public class BookParser
    {
        public static List<string> ParseSearchListFromJson(string json)
        {
            JObject o = JObject.Parse(json);
            var list = o.SelectTokens("$.list[*].title_def");
            
            return list.Select(x => x.ToString()).ToList() ;
        }
    }
}