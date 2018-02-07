using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookReader.Network;
using Xamarin.Forms;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class SearchPageModel : BasePageModel
    {
        public SearchPageModel()
        {
            BackCommand = new BaseCommand(async (args) => { await this.PopPageAsync(); });
            SearchTextChangedCommand = new BaseCommand((args) =>
            {
                Task.Factory.StartNew(() =>
                {
                    if (!(args is TextChangedEventArgs textChanged))
                    {
                        SearchList = null;
                        return;
                    }
                    SearchList = string.IsNullOrEmpty(textChanged.NewTextValue) ? null : new ObservableCollection<string>(BookManager.GetSearchList(textChanged.NewTextValue));
                });
            });

            SearchListTappedCommand = new BaseCommand(async (args) =>
            {
                var tapped = args as ItemTappedEventArgs;
                await this.PushPageAsync(this.GetPageFromCache<SearchResultPageModel>());
            });
        }

        public ICommand BackCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchTextChangedCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchListTappedCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ObservableCollection<string> SearchList
        {
            get => GetField<ObservableCollection<string>>();
            set => SetField(value);
        }

        public string SearchText
        {
            get => GetField<string>();
            set => SetField(value);
        }
    }
}