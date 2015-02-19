using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace VM204
{
	public class RelayCardDiscoveryPage : ContentPage
	{
		ListView listView;
		List<Discovery> discoveries;
		public RelayCardDiscoveryPage ()
		{
			Title = "Discovery";

			listView = new ListView {
				RowHeight = 40
			};
			discoveries = new List<Discovery> ();
			listView.ItemTemplate = new DataTemplate (typeof(TextCell));
			if (Device.OS == TargetPlatform.iOS) {
				listView.ItemsSource = new string [1]{ "" };			
			}
			listView.ItemTemplate.SetBinding (TextCell.TextProperty, "Name");
			listView.ItemTemplate.SetBinding (TextCell.DetailProperty, "IP");

			var layout = new StackLayout ();
			layout.Children.Add (listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = discoveries;
		}

		public void GetCards()
		{
			var scanner = new DiscoveryScanner();
		 	scanner.Scan ();

			scanner.DiscoveryFound += (object sender, DiscoveryFoundEventArgs e) => {
				discoveries.Add(e.Card);
				listView.ItemsSource = discoveries;

			};
		}
	}
}

