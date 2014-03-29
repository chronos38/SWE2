using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

namespace Server.BusinessLayer.Commands
{
	public class CommandTest : ICommand
	{
		public CommandTest()
		{
		}

		public RPResult Execute(RPCall call)
		{
			RPResult test = new RPResult();
			test.count = 12345;
			return test;
		}
	}
}
