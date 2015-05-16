using Xamarin.Forms;


namespace VM204
{
	public class RelayCardWebView : ContentPage
	{
		public RelayCardWebView ()
		{
			//Create the webview and set it as the content
			var webView = new WebView();
			Content = webView;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			//Bind the RelayCards Name to the title
			this.SetBinding (TitleProperty, "Name");

			//Get the context relayCard
			var webView = ((WebView)Content);
			var relayCard = ((RelayCard)BindingContext);
			//Connect to local or extern Ip address
			webView.Source = UrlBuilder.GenerateUrl(relayCard);
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			//set the site to google to disconnect from the relaycard
			var webView = ((WebView)Content);
			webView.Source="";
		}
	}
}

