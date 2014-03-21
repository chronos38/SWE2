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

			_rc.Send();
			
			Thread.Sleep(2000); // race condition... 
			Assert.AreEqual("testargument", _rs.test);
			/*
			RPCall result = (RPCall)_rs.RPCallQueue.Dequeue();
			Assert.AreEqual(test2.id, result.id);
			Assert.AreEqual(test2.procedureName, result.procedureName);
			Assert.AreEqual(test2.procedureArgs[0], result.procedureArgs[0]);
			Assert.AreEqual(test2.procedureArgs[1], result.procedureArgs[1]);
			Assert.AreEqual(test2.procedureArgs[2], result.procedureArgs[2]);

			result = (RPCall)_rs.RPCallQueue.Dequeue();
			Assert.AreEqual(test.id, result.id);
			Assert.AreEqual(test.procedureName, result.procedureName);
			Assert.AreEqual(test.procedureArgs[0], result.procedureArgs[0]);
			Assert.AreEqual(test.procedureArgs[1], result.procedureArgs[1]);
			*/
		}

	}
}
