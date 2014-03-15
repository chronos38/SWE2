using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	class TableContact : Table
	{
		public TableContact()
		{
			// variables
			Database db = Database.Instance;

			// check database state
			if (!db.IsConnected()) {
				throw new ApplicationException("Database is not connected.");
			}

			// select data table
			DataTable = db.Select("SELECT * FROM Contact");

			// check if all went successful
			if (DataTable == null) {
				throw new NullReferenceException("No tables were found.");
			}
		}
	}
}
