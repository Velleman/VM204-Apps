using System;

namespace VM204
{
	public static class UrlBuilder
	{
		/// <summary>
		/// Generates the url form a RelayCard object
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="card">Card.</param>
		public static string GenerateUrl(RelayCard card)
		{
			if (card.ConnectLocal) {
				return "http://" + card.Username + ":" + card.Password + "@" + card.LocalIp + ":" + card.LocalPort;
			} else {
				return "http://" + card.Username + ":" + card.Password+ "@" + card.ExternalIp + ":" + card.ExternalPort;
			}
		}
	}
}

