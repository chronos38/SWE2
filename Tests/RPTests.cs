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
using DataTransfer.Types;

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
			CommandDictionary.Instance.RegisterCommand("CommandUpsert", new CommandUpsert());
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
			DateTime? time = new DateTime?(new DateTime(2000,1,1));
			Contact contact = new Contact(1, "1", "teststring", "teststring", "teststring", "teststring", "teststring", time, "teststring", "teststring", "teststring", "teststring");
			await prox.SendContactAsync(contact);
		}

		[TestMethod]
		public async Task TestMethod2()
		{
			_runThread = new Thread(_rs.Run);
			_runThread.Start();
			Proxy prox = new Proxy();
			await prox.SearchContactsAsync("Max");
		}
	}
}
