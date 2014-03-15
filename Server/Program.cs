using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using Server.DAL;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Database db = Database.Instance;
			db.Connect();
			TableContact contacts = new TableContact();
			
			foreach (DataRow row in contacts.Rows) {
				foreach (object item in row.ItemArray) {
					Console.Write(String.Format("{0}", item));
				}
				Console.Write('\n');
			}

			Console.ReadKey();
		}
	}
}