using System;
using Xamarin.Forms;

namespace VM204
{
	public class App : Application
	{
		static RelayCardDatabase database;

		public static RelayCardDatabase Database{
			get { return database; }
		}

		public Page GetMainPage ()
		{	
			return MainPage;
		}
	
		public App ()
		{
			database = new RelayCardDatabase ();
			// The root page of your application
			MainPage = new NavigationPage (new RelayCardListPage ());
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

