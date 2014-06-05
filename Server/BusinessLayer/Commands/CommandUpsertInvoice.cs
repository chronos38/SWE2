using DataTransfer;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer.Commands
{
	public class CommandUpsertInvoice : ICommand
	{
		public CommandUpsertInvoice()
		{

		}

		public RPResult Execute(RPCall call)
		{
			RPResult result = new RPResult();
			result.success = DatabaseFactory.Factory().UpsertInvoice(call.dt);
			return result;
		}
	}
}
