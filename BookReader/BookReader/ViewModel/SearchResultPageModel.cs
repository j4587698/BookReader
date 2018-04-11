using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BookReader.Controller;
using BookReader.Entity;
using BookReader.View;
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
            SearchStr = searchStr + "-搜索结果";           
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await this.PushModalPageAsync(this.GetPageFromCache<LoadingPopPageModel>(), x => x.Init("正在搜索，请稍后..."));
            SearchResults = new ObservableCollection<SearchResultEntity>(_searchController.GetSearchResult(SearchStr));
        }

        public string SearchStr
        {
            get => GetField<string>();
            set => SetField(value);
        }

        public ObservableCollection<SearchResultEntity> SearchResults
        {
            get => GetField<ObservableCollection<SearchResultEntity>>();
            set =>SetField(value);
        }

    }
}