using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReader.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace BookReader.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage, IBasePage<SearchPageModel>
	{
		public SearchPage ()
		{
			InitializeComponent ();
		    NavigationPage.SetHasNavigationBar(this, false);
		    Hot.TagEntry.IsVisible = false;
		    History.TagEntry.IsVisible = false;
		}
	}
}