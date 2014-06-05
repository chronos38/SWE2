using DataTransfer;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer.Commands
{
	class CommandDeleteCompany : ICommand
	{
		public RPResult Execute(RPCall call)
		{
			RPResult result = new RPResult();
			result.success = DatabaseSingleton.Factory().DeleteCompany(Int32.Parse(call.procedureArgs[0]));
			return result;
		}
	}
}
