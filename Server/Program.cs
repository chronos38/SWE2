using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.RPC;
using Server.BusinessLayer;
using Server.BusinessLayer.Commands;

namespace Server
{
	class Program
	{

		static void Main(string[] args)
		{
			CommandDictionary.Instance.RegisterCommand("CommandTest", new CommandTest());
			CommandDictionary.Instance.RegisterCommand("CommandContact", new CommandContact());
			CommandDictionary.Instance.RegisterCommand("CommandUpsert", new CommandUpsert());
			RPServer rs = new RPServer(12345, 2);
			Thread runThread = new Thread(rs.Run);
			runThread.Start();
			Console.ReadKey();
			runThread.Abort();
			rs.Dispose();
		}
	}
}