using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReader.ViewModel;
using Xamarin.Forms;
using Xamvvm;

namespace BookReader.View
{
	public partial class MainPage : ContentPage, IBasePage<MainPageModel>
    {
		public MainPage()
		{
			InitializeComponent();
        }
	}
}
