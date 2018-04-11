using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookReader.View;
using BookReader.ViewModel;
using Xamarin.Forms;
using Xamvvm;

namespace BookReader
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            Init.InitApp();
		    var factory = new XamvvmFormsFactory(this);
		    factory.RegisterNavigationPage<MainNavigationPageModel>(() => this.GetPageFromCache<MainPageModel>());
		    XamvvmCore.SetCurrentFactory(factory);
		    MainPage = this.GetPageFromCache<MainNavigationPageModel>() as MainNavigationPage;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
