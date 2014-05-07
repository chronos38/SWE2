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
using Server.DAL;
using Tests.MockDAL;
using System.Data;

namespace Tests
{
	[TestClass]
	public class ProxyTest
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
			CommandDictionary.Instance.RegisterCommand("CommandGetCompanies", new CommandGetCompanies());
			IDatabaseSingleton.SetType<MockDB>();
		}

		[TestCleanup]
		public void CleanUp()
		{
			_runThread.Abort();
			_rs.Dispose();
		}

		[TestMethod]
		public async Task SendContactsAsyncIsSuccessful()
		{
			_runThread = new Thread(_rs.Run);
			_runThread.Start();
			Proxy prox = new Proxy();
			DateTime? time = new DateTime?(new DateTime(2000,1,1));
			Contact contact = new Contact(1, "1", "teststring", "teststring", "teststring", "teststring", "teststring", time, null, "teststring", "teststring", "teststring", "teststring");
			RPResult ret = await prox.SendContactAsync(contact);
			Assert.AreEqual(1, ret.success);
		}

		[TestMethod]
		public async Task SearchContactsAsyncReturnsCorrectData()
		{
			_runThread = new Thread(_rs.Run);
			_runThread.Start();
			Proxy prox = new Proxy();
			RPResult ret = await prox.SearchContactsAsync("Max");
			Assert.AreEqual(ret.dt.Rows[0]["Forename"], "Max");
			Assert.AreEqual(ret.dt.Rows[0]["Surname"], "Mustermann");
		}

		[TestMethod]
		public async Task GetCompaniesAsyncReturnsCorrectData()
		{
			_runThread = new Thread(_rs.Run);
			_runThread.Start();
			Proxy prox = new Proxy();
			RPResult ret = await prox.GetCompaniesAsync();
			Assert.AreEqual("teststring", ret.dt.Rows[0]["Name"]);
		}
	}
}
