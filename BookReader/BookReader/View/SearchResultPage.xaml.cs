using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReader.ViewModel;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace BookReader.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchResultPage : ContentPage, IBasePage<SearchResultPageModel>
	{
		public SearchResultPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}

	    protected override bool OnBackButtonPressed()
	    {
	        Navigation.PopAllPopupAsync();
	        return base.OnBackButtonPressed();
	    }
	}
}