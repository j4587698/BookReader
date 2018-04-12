using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BookReader.DB;
using BookReader.Entity;
using Xamarin.Forms;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class MainPageModel : BasePageModel
    {

        public MainPageModel()
        {
            SearchCommand = new BaseCommand(async (arg) =>
                {
                    await this.PushPageAsync(this.GetPageFromCache<SearchPageModel>());
                    //await this.PushModalPageAsync(this.GetPageFromCache<SearchPageModel>());
                });

            //BookInfoEntities = new ObservableCollection<BookInfoEntity>(BookManager.GetAllBookInfos());
            //测试数据
            BookInfoEntities = new ObservableCollection<BookInfoEntity>()
            {
                new BookInfoEntity()
                {
                    Author = "JX",
                    BookName = "测试图书",
                    ConverPath = "https://img4.km.com/bookimg/public/images/cover/c4ca/58bae2611976c.jpg",
                    LastChapter = "第十五章 测试章节",
                    LastModify = DateTime.Now,
                    LastReaded = "第十五章 测试章节"
                }
            };
        }
        public ICommand SearchCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ObservableCollection<BookInfoEntity> BookInfoEntities
        {
            get => GetField<ObservableCollection<BookInfoEntity>>();
            set => SetField(value);
        }
    }
}
