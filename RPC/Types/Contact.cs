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
		public DateTime? BirthDate { get; private set; }
		public string Street { get; protected set; }
		public string StreetNumber { get; protected set; }
		public string PostalCode { get; protected set; }
		public string City { get; protected set; }

		public Contact(int id, string uid, string name, string title, string fore, string sur, string suffix, DateTime? birth, string street, string number, string zip, string city)
		{
			ID = id;
			UID = uid;
			Name = name;
			Title = title;
			Forename = fore;
			Surname = sur;
			Suffix = suffix;
			BirthDate = birth;
			Street = street;
			StreetNumber = number;
			PostalCode = zip;
			City = city;
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
			result["BirthDate"] = BirthDate;
			result["Street"] = Street;
			result["StreetNumber"] = StreetNumber;
			result["PostalCode"] = PostalCode;
			result["City"] = City;

			// return
			return result;
		}

		public Contact FromDataRow(DataRow row)
		{
			string parse = row["Birthday"] as string;

			if (parse != null) {
				Birthday = DateTime.Parse(parse, null);
			} else {
				Birthday = null;
			}

			ID = (int)row["ID"];
			UID = row["UID"] as string;
			Name = row["Name"] as string;
			Title = row["Title"] as string;
			Forename = row["Forename"] as string;
			Surname = row["Surname"] as string;
			Suffix = row["Suffix"] as string;
			BirthDate = row["BirthDate"] as DateTime?;
			Street = row["Street"] as string;
			StreetNumber = row["StreetNumber"] as string;
			PostalCode = row["PostalCode"] as string;
			City = row["City"] as string;

			return this;
		}

		public Contact FromObjectArray(object[] items)
		{
			string parse = items[7] as string;

			if (parse != null) {
				BirthDate = DateTime.Parse(parse, null);
			} else {
				BirthDate = null;
			}

			ID = Int32.Parse(items[0] as string);
			UID = items[1] as string;
			Name = items[2] as string;
			Title = items[3] as string;
			Forename = items[4] as string;
			Surname = items[5] as string;
			Suffix = items[6] as string;
			//BirthDate = DateTime.Parse(items[7] as string, null);
			Street = items[8] as string;
			StreetNumber = items[9] as string;
			PostalCode = items[10] as string;
			City = items[11] as string;

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
			table.Columns.Add("BirthDate");
			table.Columns.Add("Street");
			table.Columns.Add("StreetNumber");
			table.Columns.Add("PostalCode");
			table.Columns.Add("City");
			table.TableName = "Contact";

			return table;
		}
	}
}
