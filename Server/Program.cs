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

			Table contact = Table.Contact;

			foreach (DataRow row in contact.Rows) {
				DataColumnCollection columns = row.Table.Columns;

				for (int i = 0; i < columns.Count; i++) {
					Console.Write(String.Format("{0}: {1} ", columns[i], row.ItemArray[i]));
				}
				Console.Write('\n');
			}

			Console.ReadKey();
		}
	}
}