using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class DatabaseFactory
	{
		static Type DatabaseType = null;

		public static void SetType<T>() where T : IDatabase, new()
		{
			if ((DatabaseType == null) || (DatabaseType.GetType() != typeof(T))) {
				DatabaseType = typeof(T);
			}

		}

		public static IDatabase Factory()
		{
			IDatabase db = (IDatabase)Activator.CreateInstance(DatabaseType);
			db.Connect("127.0.0.1", 5432, "sweadmin", "swe", "swedb");
			return db;
		}
	}
}
