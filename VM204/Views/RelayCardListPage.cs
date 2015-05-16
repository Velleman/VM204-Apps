using System;
using Xamarin.Forms;
using System.Threading;

namespace VM204
{
	public class RelayCardListPage : ContentPage 
	{
		ListView listView;
		public RelayCardListPage ()
		{
			Title = "Relay Cards";

			listView = new ListView {
				RowHeight = 40
			};
			listView.ItemTemplate = new DataTemplate (typeof(RelayCardCell));
			listView.ItemTemplate.SetBinding (TextCell.TextProperty, "Name");
			listView.IsPullToRefreshEnabled = true;

			listView.Refreshing += (object sender, EventArgs e) => {
				RefreshList();
			};


			if (Device.OS == TargetPlatform.iOS) {
				listView.ItemsSource = new string [1]{ "" };			
			}

			var layout = new StackLayout ();
			layout.Children.Add (listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

			ToolbarItem tbi = new ToolbarItem ("+",null,()=>{
					var relayCard = new RelayCard();
					var relayCardPage = new RelayCardPage();
					relayCardPage.BindingContext = relayCard;
					Navigation.PushAsync(relayCardPage);
			},ToolbarItemOrder.Default,0);
			
			this.ToolbarItems.Add (tbi);
		
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			RefreshList ();
			App.Database.TableChanged += (object sender, SQLite.NotifyTableChangedEventArgs e) => {
				RefreshList();
			};
		}

		public void RefreshList()
		{
			listView.ItemsSource = null;
			listView.ItemsSource = App.Database.GetCards ();
			listView.IsRefreshing = false;
		}
	}
}

