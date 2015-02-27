using System;
using Xamarin.Forms;


namespace VM204
{
	public class RelayCardPage : ContentPage
	{
		public RelayCardPage ()
		{
			this.SetBinding (Page.TitleProperty, "Name");

			NavigationPage.SetHasNavigationBar (this, true);

			//Name
			var nameLabel = new Label{ Text = "Name" };
			var nameEntry = new Entry ();
			nameEntry.SetBinding (Entry.TextProperty, "Name");
			//localIp
			var localIpLabel = new Label{ Text = "Local IP" };
			var localIpEntry = new Entry ();
			localIpEntry.SetBinding (Entry.TextProperty, "LocalIp");
			localIpEntry.Keyboard = Keyboard.Telephone;

			//localPort
			var localPortLabel = new Label{ Text = "Local port"};
			var localPortEntry = new Entry();
			localPortEntry.SetBinding (Entry.TextProperty, "LocalPort");
			localPortEntry.Keyboard = Keyboard.Numeric;

			//externalIp
			var externalIpLabel = new Label{ Text = "External IP" };
			var externalIpEntry = new Entry ();
			externalIpEntry.SetBinding (Entry.TextProperty, "ExternalIp");
			externalIpEntry.Keyboard = Keyboard.Url;

			//externalPort
			var externalPortLabel = new Label{ Text = "External port"};
			var externalPortEntry = new Entry();
			externalPortEntry.SetBinding (Entry.TextProperty, "ExternalPort");
			externalPortEntry.Keyboard = Keyboard.Numeric;

			var preferLocalLabel = new Label{ Text = "Connect to local network"};
			var preferLocalSwitch = new Switch ();
			preferLocalSwitch.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "ConnectLocal");

			var scanButton = new Button { Text = "Scan" };
			scanButton.Clicked += (object sender, EventArgs e) => {
				var discoveryPage = new RelayCardDiscoveryPage ();

				discoveryPage.BindingContext = BindingContext;

				Navigation.PushAsync(discoveryPage);
			};

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) => {
				var relayCard = (RelayCard)BindingContext;
				App.Database.SaveCard(relayCard);
				this.Navigation.PopAsync();
			};
				
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => this.Navigation.PopAsync ();

	

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(10),
				Children = {
					nameLabel,nameEntry,
					localIpLabel,localIpEntry,
					localPortLabel,localPortEntry,
					externalIpLabel,externalIpEntry,
					externalPortLabel,externalPortEntry,
					preferLocalLabel,preferLocalSwitch,
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Spacing = 50,
						Children={
							scanButton,saveButton, cancelButton
						}
					}
				}
			};


		}

	}
}

