using System;
using DanMacross;
using Xamarin.Forms;

namespace DanMacrossSample
{
	public class App : Application
	{
		public App ()
		{
            // The root page of your application
            MainPage = new ContentPage {
                Content = new MediaElement("https://animate-10012595.file.myqcloud.com/Uchuu%20Patrol%20Luluco/11.mp4"),
			};
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

