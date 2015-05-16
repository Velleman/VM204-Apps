using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace VM204
{
	public class RelayCardDiscoveryPage : ContentPage
	{
		ListView listView;
		List<RelayCard> discoveries;
		DiscoveryScanner scanner;
		public RelayCard SelectedCard { get; set;}

		public RelayCardDiscoveryPage ()
		{
			Title = "Discovery";

			scanner = new DiscoveryScanner ();

			listView = new ListView {
				RowHeight = 50
			};
			listView.HasUnevenRows = true;
			listView.ItemTemplate = new DataTemplate (typeof(TextCell));
			if (Device.OS == TargetPlatform.iOS) {
				listView.ItemsSource = new string [1]{ "" };			
			}
			listView.ItemTemplate.SetBinding (TextCell.TextProperty, "Name");
			listView.ItemTemplate.SetBinding (TextCell.DetailProperty, "LocalIp");
			listView.IsPullToRefreshEnabled = true;
			listView.Refreshing += (object sender, EventArgs e) => {
				scanner.Scan();
			};

			listView.SetBinding (ListView.IsRefreshingProperty, "isScanning");
			listView.BindingContext = scanner;

			listView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				SelectedCard = (RelayCard)e.Item;

				if(((RelayCard)Navigation.NavigationStack[1].BindingContext).ID == 0)
					Navigation.NavigationStack[1].BindingContext = SelectedCard;
				else
				{
					((RelayCard)Navigation.NavigationStack[1].BindingContext).LocalIp = SelectedCard.LocalIp;
					((RelayCard)Navigation.NavigationStack[1].BindingContext).LocalPort = SelectedCard.LocalPort;		
					((RelayCard)Navigation.NavigationStack[1].BindingContext).Name = SelectedCard.Name;		
					((RelayCard)Navigation.NavigationStack[1].BindingContext).ConnectLocal = true;
				}	
				Navigation.PopAsync(true);
			};
				
			discoveries = new List<RelayCard> ();

			scanner.DiscoveryFound += (object sender, DiscoveryFoundEventArgs e) => {
				bool isInList = false;
				foreach(RelayCard d in discoveries)
				{
					if(d.LocalIp == e.Card.IP)
					{
						isInList = true;
					}
				}
				if(!isInList)
				{
					discoveries.Add(e.Card.ConvertToRelayCard());
					listView.ItemsSource = null;
					listView.ItemsSource = discoveries;
				}
			};

			var layout = new StackLayout ();
			layout.Children.Add (listView);

			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;
			scanner.Scan ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			listView.ItemsSource = discoveries;
		}
	}
}

