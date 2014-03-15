using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	class GenericTable : Table
	{
		public GenericTable(string table)
		{
			// variables
			Database db = Database.Instance;

			// check database state
			if (!db.IsConnected()) {
				throw new ApplicationException("Database is not connected.");
			}

			// select data table
			DataTable = db.Select(String.Format("SELECT * FROM {0}", table));

			// check if all went successful
			if (DataTable == null) {
				throw new NullReferenceException("No tables were found.");
			}
		}
	}
}
