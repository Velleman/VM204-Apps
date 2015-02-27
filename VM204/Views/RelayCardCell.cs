using Xamarin.Forms;
using System;


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

			var editAction = new MenuItem { Text = "Edit" };
			editAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			editAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				var relayCardPage = new RelayCardPage();
				relayCardPage.BindingContext = (RelayCard)mi.CommandParameter;
				((ContentPage)(((StackLayout)((ListView)(this.ParentView)).ParentView).ParentView)).Navigation.PushAsync(relayCardPage);
			};

			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			deleteAction.Clicked += async (sender, e) => {
				var mi = ((MenuItem)sender);
				
				App.Database.DeleteCard (((RelayCard)mi.CommandParameter).ID);
				((RelayCardListPage)(((StackLayout)((ListView)(this.ParentView)).ParentView).ParentView)).RefreshList();
			};
				
			ContextActions.Add (editAction);
			ContextActions.Add (deleteAction);

			var layout = new StackLayout{ 
				Padding = new Thickness(20,0,0,0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {label}
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

