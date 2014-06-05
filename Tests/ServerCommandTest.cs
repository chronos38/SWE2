using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Server.MockDAL;
using Server.DAL;
using System.Data;
using Server.BusinessLayer.Commands;
using DataTransfer;
using System.Text;

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
		public void CommandContactReturnsCorrectTable()
		{
			RPCall call = new RPCall();
			call.procedureArgs = new string[] { "Max" };

			CommandContact com = new CommandContact();
			RPResult ret = com.Execute(call);
			Assert.AreEqual(ret.dt.TableName, "Contacts");
			Assert.IsTrue(TestHelper.CompareDataTables(ret.dt, _th.GetTestPersonDataTable()));
		}

		[TestMethod]
		public void CommandDeleteCompaniesDeletesCorrectAmount()
		{
			RPCall call = new RPCall();
			call.procedureArgs = new string[] { "1" };
			
			CommandDeleteCompany com = new CommandDeleteCompany();
			RPResult ret = com.Execute(call);
			Assert.AreEqual(1, ret.success);
		}

		[TestMethod]
		public void CommandGetCompaniesReturnsCorrectTable()
		{
			RPCall call = new RPCall();

			CommandGetCompanies com = new CommandGetCompanies();
			RPResult ret = com.Execute(call);
			Assert.AreEqual(ret.dt.TableName, "Companies");
			Assert.IsTrue(TestHelper.CompareDataTables(ret.dt, _th.GetTestCompanyDataTable()));
		}

		[TestMethod]
		public void CommandSearchCompanyReturnsCorrectTable()
		{
			RPCall call = new RPCall();
			call.procedureArgs = new string[] {"1", "teststring" };
			CommandSearchCompany com = new CommandSearchCompany();
			RPResult ret = com.Execute(call);
			Assert.IsTrue(TestHelper.CompareDataTables(ret.dt, _th.GetTestCompanyDataTable()));
		}

		[TestMethod]
		public void CommandGetCompanyReturnsCorrectTable()
		{
			RPCall call = new RPCall();
			call.procedureArgs = new string[] { "1" };
			CommandGetCompany com = new CommandGetCompany();
			RPResult ret = com.Execute(call);
			Assert.IsTrue(TestHelper.CompareDataTables(ret.dt, _th.GetTestCompanyDataTable()));
		}

		[TestMethod]
		public void CommandUpsertIsSuccessful()
		{
			RPCall call = new RPCall();
			call.dt = _th.GetTestPersonDataTable();
			CommandUpsert com = new CommandUpsert();
			RPResult ret = com.Execute(call);
			Assert.AreEqual(1, ret.success);
		}
	}
}
