using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTransfer;
using Server.DAL;
using DataTransfer.Types;
using DataTransfer.Converter;

namespace Server.BusinessLayer.Commands
{
	public class CommandContact : ICommand
	{
		public CommandContact()
		{
		
		}

		public RPResult Execute(RPCall call)
		{
			if (call.procedureArgs == null || call.procedureArgs.Length < 1) {
				throw new InvalidOperationException();
			}
			List<Contact> contacts = Database.Factory.SearchContacts(call.procedureArgs[0]);
			ContactListConverter conv = new ContactListConverter();

			RPResult retVal = new RPResult();
			retVal.dt = (DataTable) conv.ConvertTo(null, null, contacts, typeof(DataTable));
			retVal.dt.TableName = "Contacts";
			return retVal;
		}
	}
}
