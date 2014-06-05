using DataTransfer;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer.Commands
{
	class CommandSetCompany : ICommand
	{
		public RPResult Execute(RPCall call)
		{
			RPResult result = new RPResult();
			result.dt = DatabaseFactory.Factory().SetCompany(Int32.Parse(call.procedureArgs[0]));
			result.dt.TableName = "Company";
			return result;
		}
	}
}
