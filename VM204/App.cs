using System;
using Xamarin.Forms;

namespace VM204
{
	public class App
	{
		static RelayCardDatabase database;

		public static RelayCardDatabase Database{
			get { return database; }
		}

		public static Page GetMainPage ()
		{	
			database = new RelayCardDatabase ();

			var mainNav = new NavigationPage(new RelayCardListPage());

			return mainNav;
		}

	}
}

