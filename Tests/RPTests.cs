using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using DataTransfer;
using Server.BusinessLayer;
using Server.BusinessLayer.Commands;
using Server.RPC;
using Client.RPC;

namespace Tests
{
	[TestClass]
	public class RPTests
	{
		RPServer _rs;
		Thread _runThread;

		[TestInitialize]
		public void Setup()
		{
			_rs = new RPServer(12345, 2);
			CommandDictionary.Instance.RegisterCommand("CommandTest", new CommandTest());
			CommandDictionary.Instance.RegisterCommand("CommandContact", new CommandContact());
		}

		[TestCleanup]
		public void CleanUp()
		{
			_runThread.Abort();
			_rs.Dispose();
		}

		[TestMethod]
		public async Task TestMethod1()
		{
			_runThread = new Thread(_rs.Run);
			_runThread.Start();
			Proxy prox = new Proxy();
			await prox.SearchContactsAsync("Max");
		}

	}
}
