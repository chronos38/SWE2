﻿using System;
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

		[TestInitialize]
		public void Setup()
		{
			_rs = new RPServer();
			_rc = new RPClient();

		}

		[TestMethod]
		public void TestMethod1()
		{

			RPCall test = new RPCall();
			test.id = 1;
			test.procedureName = "test";
			test.procedureArgs = new string[]{"asd", "abc"};

			_rc.Send(test);
			Thread.Sleep(2000); // race condition... 
			RPCall result = (RPCall)_rs.RPCallQueue.Dequeue();
			Assert.AreEqual(test.id, result.id);
			Assert.AreEqual(test.procedureName, result.procedureName);
			Assert.AreEqual(test.procedureArgs[0], result.procedureArgs[0]);
			Assert.AreEqual(test.procedureArgs[1], result.procedureArgs[1]);
		}
	}
}