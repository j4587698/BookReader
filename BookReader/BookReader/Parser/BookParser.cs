using System.Collections.Generic;

namespace BookReader.Parser
{
    public class BookParser
    {
        public static List<string> ParseSearchList(string json)
        {
            return JsonParser.GetList(json, "$.list[*].title_def");
        }
    }
}