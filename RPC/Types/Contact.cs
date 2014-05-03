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
		public int ID { get; private set; }
		public string UID { get; private set; }
		public string Name { get; private set; }
		public string Title { get; private set; }
		public string Forename { get; private set; }
		public string Surname { get; private set; }
		public string Suffix { get; private set; }
		public DateTime? Birthday { get; private set; }
		public Address Address { get; private set; }
		public List<AdditionalAddress> AdditionalAddresses { get; private set; }

		public Contact(int id, string uid, string name, string title, string fore, string sur, string suffix, DateTime? birth, Address address, List<AdditionalAddress> addresses)
		{
			ID = id;
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
			result["ID"] = ID;
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
			ID = (int)row["ID"];
			UID = row["UID"] as string;
			Name = row["Name"] as string;
			Title = row["Title"] as string;
			Forename = row["Forename"] as string;
			Surname = row["Surname"] as string;
			Suffix = row["Suffix"] as string;
			Birthday = row["Birthday"] as DateTime?;
			Address = row["Address"] as Address;
			AdditionalAddresses = row["AddtionalAddresses"] as List<AdditionalAddress>;

			return this;
		}

		public Contact FromObjectArray(object[] items)
		{
			string parse = items[7] as string;

			if (parse != null) {
				Birthday = DateTime.Parse(parse, null);
			} else {
				Birthday = null;
			}

			ID = Int32.Parse(items[0] as string);
			UID = items[1] as string;
			Name = items[2] as string;
			Title = items[3] as string;
			Forename = items[4] as string;
			Surname = items[5] as string;
			Suffix = items[6] as string;
			//Birthday = DateTime.Parse(items[7] as string, null);
			Address = items[8] as Address;
			AdditionalAddresses = items[9] as List<AdditionalAddress>;

			return this;
		}

		public static DataTable CreateTable()
		{
			// variables
			DataTable table = new DataTable();

			// add columns
			table.Columns.Add("ID");
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
