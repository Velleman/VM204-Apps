using System;
using SQLite;
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
		public string ExternalIp { get; set;}
		public bool PrefferedLocal { get; set;}
	}
}

