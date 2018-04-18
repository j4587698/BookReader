using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookReader.Controller;
using BookReader.Entity;
using BookReader.View;
using Rg.Plugins.Popup.Services;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class SearchResultPageModel : BasePageModel
    {

        private readonly SearchController _searchController;

        public SearchResultPageModel()
        {
            _searchController = new SearchController();

            BookListTappedCommand = new BaseCommand((args) =>
            {
                this.PushPageAsync(this.GetPageFromCache<ReadingPageModel>());

            });
        }

        public async Task InitAsync(string searchStr)
        {
            SearchStr = searchStr + "-搜索结果";
            var loadingPopPage = new LoadingPopPage("正在搜索，请稍后...");
            await PopupNavigation.Instance.PushAsync(loadingPopPage);
            SearchResults = new ObservableCollection<SearchResultEntity>(_searchController.GetSearchResult(searchStr));
            await PopupNavigation.Instance.RemovePageAsync(loadingPopPage);
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

        public ICommand BookListTappedCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

    }
}