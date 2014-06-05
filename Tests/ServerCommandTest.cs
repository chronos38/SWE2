using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Server.MockDAL;
using Server.DAL;
using System.Data;
using Server.BusinessLayer.Commands;
using DataTransfer;

namespace Tests
{
	[TestClass]
	public class ServerCommandTest
	{
		TestHelper _th = new TestHelper();

		[TestInitialize]
		public void Setup()
		{
			DatabaseFactory.SetType<MockDB>();
		}

		[TestMethod]
		public void CommandContact()
		{
			RPCall call = new RPCall();
			call.procedureArgs = new string[] { "Max" };

			CommandContact com = new CommandContact();
			RPResult ret = com.Execute(call);
			Assert.AreEqual(ret.dt.TableName, "Contacts");
			Assert.IsTrue(TestHelper.CompareDataTables(ret.dt, _th.GetTestPersonDataTable()));
		}
	}
}
