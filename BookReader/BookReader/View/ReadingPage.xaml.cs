using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReader.CustomeView;
using BookReader.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace BookReader.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReadingPage : FullSrceenPage, IBasePage<ReadingPageModel>
	{
		public ReadingPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
		    this.SetBinding(IsFullSrceenProperty, "IsFullSrceen", BindingMode.TwoWay);
            InitializeComponent ();
            
		}
        
	}
}