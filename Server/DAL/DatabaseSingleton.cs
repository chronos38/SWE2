using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class DatabaseSingleton
	{
		static IDatabase DB = null;

		public static void SetType<T>() where T : IDatabase, new()
		{
			if ((DB == null) || (DB.GetType() != typeof(T))) {
				DB = new T();
			}

		}
		public static IDatabase Instance()
		{
			if ((DB == null)) {
				SetType<Database>();
			}
			/* TODO: Connection Parameter should not be hardcoded*/
			DB.Connect("127.0.0.1", 5432, "sweadmin", "swe", "swedb"); 
			return DB;
		}

		public static IDatabase Factory()
		{
			IDatabase db = new Database();
			db.Connect("127.0.0.1", 5432, "sweadmin", "swe", "swedb");
			return db;
		}
	}
}
