using Xamarin.Forms;


namespace VM204
{
	public class RelayCardWebView : ContentPage
	{
		public RelayCardWebView ()
		{
			var webView = new WebView();

			Content = webView;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var webView = ((WebView)Content);
			var relayCard = ((RelayCard)BindingContext);
			if(relayCard.ConnectLocal)
				webView.Source = "http://" + relayCard.LocalIp + ":" + relayCard.LocalPort;
			else
				webView.Source = "http://" + relayCard.ExternalIp + ":" + relayCard.ExternalPort;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			var webView = ((WebView)Content);
			webView.Source="";

		}
	}
}

