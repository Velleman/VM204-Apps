using System;
using Xamarin.Forms;


namespace VM204
{
	public class RelayCardPage : ContentPage
	{
		public RelayCardPage ()
		{
			//Bind the name of the card to the title of the page
			this.SetBinding (Page.TitleProperty, "Name");

			//Enable NavigationBar
			NavigationPage.SetHasNavigationBar (this, true);

			// Create the Name Section
			var nameSection = new TableSection ();
			nameSection.Title = "Name of the card";
			//Create the name cell and bind it to the Name 
			var nameCell = new EntryCell ();
			nameCell.SetBinding (EntryCell.TextProperty, "Name");
			nameCell.Label = "Name of the Relay Card";
			//add the name cell to the section
			nameSection.Add(nameCell);
			//Create the username cell and bind it to the Username 
			var loginCell = new EntryCell ();
			loginCell.SetBinding (EntryCell.TextProperty, "Username");
			loginCell.Label = "Username for authentication";
			//add the name cell to the section
			nameSection.Add(loginCell);
			//Create the username cell and bind it to the Username 
			var passwordCell = new EntryCell ();
			passwordCell.SetBinding (EntryCell.TextProperty, "Password");
			passwordCell.Label = "Password for authentication";
			//add the name cell to the section
			nameSection.Add(passwordCell);




			//Local Table Section
			var localNetworkSection = new TableSection ();
			localNetworkSection.Title = "Local Network Settings";
			var localIPCell = new EntryCell ();
			localIPCell.Label = "Local IP:";
			localIPCell.SetBinding (EntryCell.TextProperty, "LocalIp");
			var localPortCell = new EntryCell ();
			localPortCell.Label = "Local Port:";
			localPortCell.SetBinding (EntryCell.TextProperty, "LocalPort");
			localPortCell.Keyboard = Keyboard.Numeric;
			localNetworkSection.Add (localIPCell);
			localNetworkSection.Add (localPortCell);


			//External Table Section
			var externalNetworkSection = new TableSection ();
			externalNetworkSection.Title = "Extern Network Settings";
			var externIPCell = new EntryCell ();
			externIPCell.Label = "External IP:";
			externIPCell.SetBinding (EntryCell.TextProperty, "ExternalIp");
			var externPortCell = new EntryCell ();
			externPortCell.Label = "External Port:";
			externPortCell.SetBinding (EntryCell.TextProperty, "ExternalPort");
			externPortCell.Keyboard = Keyboard.Numeric;
			externalNetworkSection.Add (externIPCell);
			externalNetworkSection.Add (externPortCell);

			var localConnectSection = new TableSection ();
			localConnectSection.Title = "CONNECT TO LOCAL IP";
			var localConnectCell = new SwitchCell ();
			localConnectCell.Text = "Connect to local network first:";
			localConnectCell.SetBinding (SwitchCell.OnProperty, "ConnectLocal");
			localConnectSection.Add (localConnectCell);

			//Create Scan Button
			var scanButton = new Button { Text = "Scan" };
			//
			scanButton.Clicked += (object sender, EventArgs e) => {
				var discoveryPage = new RelayCardDiscoveryPage ();

				discoveryPage.BindingContext = BindingContext;

				Navigation.PushAsync(discoveryPage);
			};
				
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => this.Navigation.PopAsync ();

			Content = new StackLayout{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
				new TableView {
				Intent = TableIntent.Form,
				Root= new TableRoot("RelayCard"){
					nameSection,
					localNetworkSection,
					externalNetworkSection,
					localConnectSection
					}
					},scanButton 
				}
			};


		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			var card = (RelayCard)BindingContext;
			SaveInDatabase (card);
		}

		private async void SaveInDatabase(RelayCard card)
		{
			if (card.ID == 0) {
				var cards = App.Database.GetCards ();
				foreach (RelayCard c in cards) {
					if (c.MacAddress == card.MacAddress) {
						var yes = await DisplayAlert ("Duplication", "This same card is already in the list overwrite?", "Yes", "No");
						if (yes) {
							card.MacAddress = c.MacAddress;
							App.Database.SaveCard (card);
						}
					}
				}
			} else {
				App.Database.SaveCard (card);
			}
		}

	}
}

