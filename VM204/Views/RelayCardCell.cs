using Xamarin.Forms;


namespace VM204
{
	public class RelayCardCell : ViewCell
	{
		/// <summary>
		/// For custom renderer on Android (only)
		/// </summary>
		public class ListButton : Button {}

		public RelayCardCell ()
		{
			var label = new Label{ 
				YAlign = TextAlignment.Center
			};
			label.SetBinding(Label.TextProperty, "Name");

			this.Tapped += (sender, e) => {
				var b = (RelayCardCell)sender;
				var t = (RelayCard)b.BindingContext;
				var relayCardWebView = new RelayCardWebView();
				relayCardWebView.BindingContext = t;
				((ContentPage)((StackLayout)((ListView)b.ParentView).ParentView).ParentView).Navigation.PushAsync(relayCardWebView);
			};

			var button = new ListButton {
				Text = ">",
				HorizontalOptions = LayoutOptions.EndAndExpand
			};
			button.SetBinding (Button.CommandParameterProperty, new Binding ("."));
			button.Clicked += (sender, e) => {
				var b = (Button)sender;
				var t = (RelayCard)b.CommandParameter;
				var relayCardPage = new RelayCardPage();
				relayCardPage.BindingContext = t;
				((ContentPage)(((StackLayout)((ListView)((StackLayout)b.ParentView).ParentView).ParentView).ParentView)).Navigation.PushAsync(relayCardPage);
			};


			var layout = new StackLayout{ 
				Padding = new Thickness(20,0,0,0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {label,button}
			};
			View = layout;

		}

		protected override void OnBindingContextChanged ()
		{
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

