using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Types
{
	public class Contact
	{
		public string UID { get; private set; }
		public string Name { get; private set; }
		public string Title { get; private set; }
		public string Forename { get; private set; }
		public string Surname { get; private set; }
		public string Suffix { get; private set; }
		public DateTime Birthday { get; private set; }
		public Address Address { get; private set; }
		public List<AdditionalAddress> AdditionalAddresses { get; private set; }

		public Contact(string uid, string name, string title, string fore, string sur, string suffix, DateTime birth, Address address, List<AdditionalAddress> addresses)
		{
			UID = uid;
			Name = name;
			Title = title;
			Forename = fore;
			Surname = sur;
			Suffix = suffix;
			Birthday = birth;
			Address = address;
			AdditionalAddresses = addresses;
		}

		public Contact(DataRow row)
		{
			FromDataRow(row);
		}

		public Contact(object[] items)
		{
			FromObjectArray(items);
		}

		public DataRow ToDataRow(DataTable table)
		{
			// variables
			DataRow result = table.NewRow();

			// create entries
			result["UID"] = UID;
			result["Name"] = Name;
			result["Title"] = Title;
			result["Forename"] = Forename;
			result["Surname"] = Surname;
			result["Suffix"] = Suffix;
			result["Birthday"] = Birthday;
			result["Address"] = Address;
			result["AdditionalAddresses"] = AdditionalAddresses;

			// return
			return result;
		}

		public Contact FromDataRow(DataRow row)
		{
			UID = row["UID"] as string;
			Name = row["Name"] as string;
			Title = row["Title"] as string;
			Forename = row["Forename"] as string;
			Surname = row["Surname"] as string;
			Suffix = row["Suffix"] as string;
			Birthday = (DateTime)row["Birthday"];
			Address = row["Address"] as Address;
			AdditionalAddresses = row["AddtionalAddresses"] as List<AdditionalAddress>;

			return this;
		}

		public Contact FromObjectArray(object[] items)
		{
			UID = items[0] as string;
			Name = items[1] as string;
			Title = items[2] as string;
			Forename = items[3] as string;
			Surname = items[4] as string;
			Suffix = items[5] as string;
			Birthday = DateTime.Parse(items[6] as string, null);
			Address = items[7] as Address;
			AdditionalAddresses = items[8] as List<AdditionalAddress>;

			return this;
		}

		public static DataTable CreateTable()
		{
			// variables
			DataTable table = new DataTable();

			// add columns
			table.Columns.Add("UID");
			table.Columns.Add("Name");
			table.Columns.Add("Title");
			table.Columns.Add("Forename");
			table.Columns.Add("Surname");
			table.Columns.Add("Suffix");
			table.Columns.Add("Birthday");
			table.Columns.Add("Address");
			table.Columns.Add("AdditionalAddresses");
			table.TableName = "Contact";

			return table;
		}
	}
}
