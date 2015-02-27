using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using VM204;

namespace VM204
{
	public class RelayCardDatabase 
	{
		static object locker = new object ();

		SQLiteConnection database;

		string DatabasePath {
			get { 
				var sqliteFilename = "RelayCardSQLite.db3";
				#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
				#else
				#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
				#else
				// WinPhone
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
				#endif
				#endif
				return path;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public RelayCardDatabase()
		{
			database = new SQLiteConnection (DatabasePath);
			// create the tables
			database.CreateTable<RelayCard>();
		}

		public IEnumerable<RelayCard> GetCards ()
		{
			lock (locker) {
				return (from i in database.Table<RelayCard>() select i).ToList();
			}
		}

		public RelayCard GetCard (int id) 
		{
			lock (locker) {
				return database.Table<RelayCard>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int SaveCard (RelayCard item) 
		{
			lock (locker) {
				if (item.ID != 0) {
					database.Update(item);
					return item.ID;
				} else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteCard(int id)
		{
			lock (locker) {
				return database.Delete<RelayCard>(id);
			}
		}
	}
}

