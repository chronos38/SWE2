using DataTransfer;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer.Commands
{
	class CommandSearchCompany : ICommand
	{
		public RPResult Execute(RPCall call)
		{
			RPResult result = new RPResult();
			result.dt = DatabaseSingleton.Factory().SearchCompany(Int32.Parse(call.procedureArgs[0]), call.procedureArgs[1]);
			result.dt.TableName = "Companies";
			return result;
		}
	}
}
