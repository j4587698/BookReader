using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReader.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace BookReader.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPopPage : PopupPage
	{
		public LoadingPopPage (string loadingMsg)
		{
            InitializeComponent ();
		    LoadingMsg.Text = loadingMsg;
		}

	    protected override bool OnBackButtonPressed()
	    {
	        return true;
	    }
	}
}