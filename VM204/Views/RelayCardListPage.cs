using System;
using Xamarin.Forms;

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

			if (Device.OS == TargetPlatform.iOS) {
				listView.ItemsSource = new string [1]{ "" };			
			}

			listView.ItemSelected += (sender, e) => {
				var relayCard = (RelayCard)e.SelectedItem;
				var relayCardPage = new RelayCardPage();
				relayCardPage.BindingContext = relayCard;
				Navigation.PushAsync(relayCardPage);
			};

			var layout = new StackLayout ();
			layout.Children.Add (listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

			ToolbarItem tbi = null;
			if (Device.OS - TargetPlatform.iOS) {
				tbi = new ToolbarItem ("+",null,()=>{
					var relayCard = new RelayCard();
					var relayCardPage = new RelayCardPage();
					relayCardPage.BindingContext = relayCard;
					Navigation.PushAsync(relayCardPage);
				},0,0);
			}
			if (Device.OS - TargetPlatform.Android) {
				tbi = new ToolbarItem ("+","plus",()=>{
					var relayCard = new RelayCard();
					var relayCardPage = new RelayCardPage();
					relayCardPage.BindingContext = relayCard;
					Navigation.PushAsync(relayCardPage);
				},0,0);
			}
			ToolbarItems.Add (tbi);

		
		}
	}
}

