using System;
using SQLite;
using Xamarin.Forms;
using System.Diagnostics;


namespace VM204
{
	public class RelayCardDiscovery
	{
		public RelayCardDiscovery ()
		{
		}
			
		public string HostName { get; set;}
		public string IpAddress { get; set;}
		public int PortWebserver { get; set; }
		public string MacAddress { get; set;}
		public string Version { get; set;}

	}
}

