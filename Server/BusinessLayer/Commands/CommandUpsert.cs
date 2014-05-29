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
				DatabaseSingleton.Instance().UpsertContact(contact[0]);
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
				result.Add(new Contact(row));
			}

			return result;
		}
	}
}
