using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using Server.DAL;
using DataTransfer.Types;
using System.Collections.Generic;
using Tests.Server.MockDAL;

namespace Tests
{
	[TestClass]
	public class MockTest
	{
		[TestMethod]
		public void DBSingletonReturnsMockDatabase()
		{
			IDatabaseSingleton.SetType<MockDB>();
			IDatabase db = IDatabaseSingleton.Instance();
			Assert.AreEqual(typeof(MockDB), db.GetType());
		}

		[TestMethod]
		public void DBSingletonReturnsDatabase()
		{
			IDatabaseSingleton.SetType<Database>();
			IDatabase db = IDatabaseSingleton.Instance();
			Assert.AreEqual(typeof(Database), db.GetType());
		}

		[TestMethod]
		public void DBSingletonChangesType()
		{
			IDatabaseSingleton.SetType<MockDB>();
			IDatabase db = IDatabaseSingleton.Instance();
			Assert.AreEqual(typeof(MockDB), db.GetType());

			IDatabaseSingleton.SetType<Database>();
			db = IDatabaseSingleton.Instance();
			Assert.AreEqual(typeof(Database), db.GetType());
		}
	}
}
