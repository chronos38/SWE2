using DataTransfer;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer.Commands
{
	class CommandDeleteInvoiceItem : ICommand
	{
		public RPResult Execute(RPCall call)
		{
			RPResult result = new RPResult();
			result.success = DatabaseFactory.Factory().DeleteInvoiceItem(BitConverter.ToInt32(call.Buffer, 0));
			return result;
		}
	}
}
