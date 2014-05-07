using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTransfer;
using DataTransfer.Types;
using DataTransfer.Converter;
using Server.DAL;
using System.Xml.Serialization;
using System.IO;

namespace Server.BusinessLayer.Commands
{
	public class CommandUpsert : ICommand
	{
		public CommandUpsert()
		{

		}

		public RPResult Execute(RPCall call)
		{
			try {
				List<Contact> contact = CreateContactList(call.dt);
				IDatabaseSingleton.Instance().UpsertContact(contact[0]);
				RPResult retVal = new RPResult();
				retVal.success = 1;
				return retVal;

			} catch (Exception ex) {
				if (ex is InvalidOperationException || ex is ArgumentNullException) {
					RPResult retval = new RPResult();
					retval.success = 0;
					return retval;
				}
				throw;
			}
		}

		private List<Contact> CreateContactList(DataTable contacts)
		{
			// variables
			List<Contact> result = new List<Contact>();

			foreach (DataRow row in contacts.Rows) {
				int id = Convert.ToInt32(row["ID"]);
				string uid = (row["UID"].GetType().Name == "DBNull" ? null : (string)row["UID"]);
				string name = (row["Name"].GetType().Name == "DBNull" ? null : (string)row["Name"]);
				string forename = (row["Forename"].GetType().Name == "DBNull" ? null : (string)row["Forename"]);
				string surname = (row["Surname"].GetType().Name == "DBNull" ? null : (string)row["Surname"]);
				string title = (row["Title"].GetType().Name == "DBNull" ? null : (string)row["Title"]);
				string suffix = (row["Suffix"].GetType().Name == "DBNull" ? null : (string)row["Suffix"]);
				DateTime? birth = (row["BirthDate"].GetType().Name == "DBNull" ? new Nullable<DateTime>() : DateTime.Parse((string)row["BirthDate"]));
				string street = (row["Street"].GetType().Name == "DBNull" ? null : row["Street"] as string);
				string number = (row["StreetNumber"].GetType().Name == "DBNull" ? null : row["StreetNumber"] as string);
				string code = (row["PostalCode"].GetType().Name == "DBNull" ? null : row["PostalCode"] as string);
				string city = (row["City"].GetType().Name == "DBNull" ? null : row["City"] as string);

				result.Add(new Contact(id, uid, name, title, forename, surname, suffix, birth, street, number, code, city));
			}

			return result;
		}
	}
}
