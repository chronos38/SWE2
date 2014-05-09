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
using Tests.Server.MockDAL;
using System.Data;
using Tests.Client.RPC;

namespace Tests
{
	[TestClass]
	public class ProxyTest
	{
		Proxy _prox;

		[TestInitialize]
		public void Setup()
		{
			_prox = new Proxy(new MockRPClient());
		}

		[TestMethod]
		public async Task SendContactsAsyncIsSuccessful()
		{
			DateTime? time = new DateTime?(new DateTime(2000,1,1));
			Contact contact = new Contact(1, "1", "teststring", "teststring", "teststring", "teststring", "teststring", time, null, "teststring", "teststring", "teststring", "teststring");
			RPResult ret = await _prox.SendContactAsync(contact);
			Assert.AreEqual(1, ret.success);
		}

		[TestMethod]
		public async Task SearchContactsAsyncReturnsCorrectData()
		{
			Proxy prox = new Proxy();
			RPResult ret = await _prox.SearchContactsAsync("Max");
			Assert.AreEqual(ret.dt.Rows[0]["Forename"], "Max");
			Assert.AreEqual(ret.dt.Rows[0]["Surname"], "Mustermann");
		}

		[TestMethod]
		public async Task GetCompaniesAsyncReturnsCorrectData()
		{
			Proxy prox = new Proxy();
			RPResult ret = await _prox.GetCompaniesAsync();
			Assert.AreEqual("teststring", ret.dt.Rows[0]["Name"]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task SearchContactsAsyncThrowsExceptionWhenStringEmpty()
		{
			Proxy prox = new Proxy();
			RPResult ret = await _prox.SearchContactsAsync("");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task SearchContactsAsyncThrowsExceptionWhenNull()
		{
			Proxy prox = new Proxy();
			RPResult ret = await _prox.SearchContactsAsync(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task SendContactAsyncThrowsExceptionWhenNull()
		{
			Proxy prox = new Proxy();
			RPResult ret = await _prox.SendContactAsync(null);
		}
	}
}
