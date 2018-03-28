using System.Collections.ObjectModel;
using BookReader.Controller;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class SearchResultPageModel : BasePageModel
    {

        private readonly SearchController _searchController;

        public SearchResultPageModel()
        {
            _searchController = new SearchController();
        }

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