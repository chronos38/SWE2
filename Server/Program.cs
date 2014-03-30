using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.RPC;

namespace Server
{
	class Program
	{

		static void Main(string[] args)
		{
			RPServer rs = new RPServer(12345, 2);
			Thread runThread = new Thread(rs.Run);
			runThread.Start();
			Console.ReadKey();
			runThread.Abort();
			rs.Dispose();
		}
	}
}