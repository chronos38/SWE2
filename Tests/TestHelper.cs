using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;
using System.Data;
using DataTransfer.Converter;

namespace Tests
{
	
	class TestHelper
	{
		ContactListConverter _conv = new ContactListConverter();

		public Contact GetTestPersonContact()
		{
			DateTime time = new DateTime(1988,8,1);
			return new Contact(2, null, "Mustermann", null, "Max", "Mustermann", null, time, null, "Musterstrasse", "1", "54321", "Musterstadt");
		}

		public Contact GetTestCompanyContact()
		{
			return new Contact(1, "1", "teststring", null, null, null, null, null, null, "teststring", "teststring", "teststring", "teststring");
		}

		public DataTable GetTestPersonDataTable()
		{
			List<Contact> contacts = new List<Contact>() { GetTestPersonContact()};
			DataTable dt = new DataTable();
			dt = (DataTable)_conv.ConvertTo(null, null, contacts, typeof(DataTable));
			// Wahrscheinlich besser den dt händisch zu befüllen Zwecks entkoppelung von ContactListConverter
			dt.TableName = "Contacts";
			return dt;
		}
		public DataTable GetTestCompanyDataTable()
		{
			List<Contact> contacts = new List<Contact>() { GetTestCompanyContact()};
			DataTable dt = new DataTable();
			dt = (DataTable)_conv.ConvertTo(null, null, contacts, typeof(DataTable));
			// Siehe oben
			return dt;
		}

		public static bool CompareDataTables(DataTable dt1, DataTable dt2)
		{
			if ((dt1 == null) && (dt2 == null)) {
				return true;
			} else if((dt1 != null) && (dt2 != null)) {

				if(dt1.Rows.Count != dt2.Rows.Count) {
					return false;
				}

				for (int i = 0; i < dt1.Rows.Count; i++)
				{
					for(int j = 0; j<dt1.Columns.Count; j++)
					{
						if (dt1.Rows[i][j].ToString() != dt2.Rows[i][j].ToString())
							return false;
					}
				}
				return true;
			}
			return false;
		}

	}
}
