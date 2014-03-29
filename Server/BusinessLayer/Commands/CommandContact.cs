using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;
using Server.DAL;

namespace Server.BusinessLayer.Commands
{
	public class CommandContact : ICommand
	{
		public CommandContact()
		{
		
		}

		public RPResult Execute(RPCall call)
		{
			Database db = Database.Factory;
			db.Connect();
			RPResult retVal = new RPResult();
			//retVal.dt = Table.Contact.DataTable;
			retVal.dt.TableName = "Contacts";
			return retVal;
		}
	}
}
