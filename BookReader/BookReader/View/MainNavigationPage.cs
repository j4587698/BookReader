using System;
using System.Collections.Generic;
using System.Text;
using BookReader.ViewModel;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamvvm;

namespace BookReader.View
{
    public class MainNavigationPage : IconNavigationPage, IBasePage<MainNavigationPageModel>
    {
        public MainNavigationPage(Page root) : base(root)
        {
        }
    }
}
