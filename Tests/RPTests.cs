using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using RPC;

namespace Tests
{
	[TestClass]
	public class RPTests
	{
		RPServer _rs;
		RPClient _rc;
		Thread _runThread;

		[TestInitialize]
		public void Setup()
		{
			_rs = new RPServer(12345, 2);
			_rc = new RPClient();
		}

		[TestCleanup]
		public void CleanUp()
		{
			_runThread.Abort();
			_rc.Dispose();
			_rs.Dispose();
		}

		[TestMethod]
		public void TestMethod1()
		{
			_runThread = new Thread(_rs.Run);
			_runThread.Start();

			RPCall test = new RPCall();
			test.procedureName = "test";
			test.procedureArgs = new string[]{"arg1", "arg2"};

			_rc.Send(test);
			
		}

	}
}
