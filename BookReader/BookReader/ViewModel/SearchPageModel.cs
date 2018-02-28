using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookReader.Controller;
using BookReader.DB;
using BookReader.Entity;
using Xamarin.Forms;
using Xamvvm;
using BookManager = BookReader.Network.BookManager;

namespace BookReader.ViewModel
{
    public class SearchPageModel : BasePageModel
    {
        public SearchPageModel()
        {
            BackCommand = new BaseCommand(async (args) => { await this.PopModalPageAsync(); });
            SearchTextChangedCommand = new BaseCommand((args) =>
            {
                Task.Factory.StartNew(() =>
                {
                    if (!(args is TextChangedEventArgs textChanged))
                    {
                        SearchList = null;
                        return;
                    }
                    SearchList = string.IsNullOrEmpty(textChanged.NewTextValue) ? null : new ObservableCollection<string>(SearchController.GetSearchList(textChanged.NewTextValue));
                });
            });

            SearchListTappedCommand = new BaseCommand<ItemTappedEventArgs>(async (args) =>
            {
                if (args != null)
                {
                    await NavToSearchResult(args.Item as string);
                }
                
            });

            HistoryTappedCommand = new BaseCommand<HistoryEntity>(async entity =>
            {
                if (entity != null)
                {
                    await NavToSearchResult(entity.Name);
                }   
            });

            ClearHistoryCommand = new BaseCommand(args =>
            {
                SearchManager.DelAllHistory();
                HistoryItems = new ObservableCollection<HistoryEntity>();
            });

            SearchCommand = new BaseCommand(async args =>
            {
                await NavToSearchResult(SearchText);
            });

            HistoryItems = new ObservableCollection<HistoryEntity>(SearchManager.GetAllHistory());
        }

        private async Task NavToSearchResult(string entityStr)
        {
            SearchManager.AddHistory(entityStr);
            HistoryItems = new ObservableCollection<HistoryEntity>(SearchManager.GetAllHistory());
            SearchText = "";
            await this.PushModalPageAsync(this.GetPageFromCache<SearchResultPageModel>(), x => x.Init(entityStr));
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

        public ICommand HistoryTappedCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchListTappedCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand ClearHistoryCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchComplateCommand
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

        public ObservableCollection<HistoryEntity> HistoryItems
        {
            get => GetField<ObservableCollection<HistoryEntity>>();
            set => SetField(value);
        }
    }
}