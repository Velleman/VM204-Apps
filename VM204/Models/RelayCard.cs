using System;
using SQLite;
using Xamarin.Forms;
using System.Diagnostics;


namespace VM204
{
	public class RelayCard
	{
		public RelayCard ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set;}
		public string Name { get; set;}
		public string LocalIp { get; set;}
		public string LocalPort { get; set;}
		public string ExternalIp { get; set;}
		public string ExternalPort { get; set;}
		public bool ConnectLocal { get; set;}
	}
}

