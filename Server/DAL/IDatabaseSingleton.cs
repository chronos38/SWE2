using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class IDatabaseSingleton
	{
		static IDatabase DB = null;
		public static IDatabase Instance<T>() where T : IDatabase, new()
		{
			if ((DB == null) || (DB.GetType() != typeof(T))) {
				DB = new T();
			}
			/* TODO: Connection Parameter should not be hardcoded*/
			DB.Connect("127.0.0.1", 5432, "sweadmin", "swe", "swedb"); 
			return DB;
		}
	}
}
