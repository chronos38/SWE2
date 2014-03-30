using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class Contact
	{
		public int ID { get; private set; }
		public string Name { get; private set; }
		public string Title { get; private set; }
		public string Forename { get; private set; }
		public string Surname { get; private set; }
		public string Suffix { get; private set; }
		public DateTime Birthday { get; private set; }
		public Address Address { get; private set; }
		public List<AdditionalAddress> AdditionalAddresses { get; private set; }

		public Contact(int id, string name, string title, string fore, string sur, string suffix, DateTime birth, Address address, List<AdditionalAddress> addresses)
		{
			ID = id;
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

		public DataRow ToDataRow(DataTable table)
		{
			// variables
			DataRow result = table.NewRow();

			// create entries
			result["ID"] = ID;
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

		public static DataTable CreateTable()
		{
			// variables
			DataTable table = new DataTable();

			// add columns
			table.Columns.Add("ID");
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
