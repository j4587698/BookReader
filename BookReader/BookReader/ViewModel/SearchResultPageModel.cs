using System.Collections.ObjectModel;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class SearchResultPageModel : BasePageModel
    {
        public void Init(string searchStr)
        {
            SearchStr = searchStr;
        }

        public string SearchStr
        {
            get => GetField<string>();
            set => SetField(value);
        }
    }
}