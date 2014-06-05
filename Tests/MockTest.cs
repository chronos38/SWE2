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
			DatabaseFactory.SetType<MockDB>();
			IDatabase db = DatabaseFactory.Factory();
			Assert.AreEqual(typeof(MockDB), db.GetType());
		}

		[TestMethod]
		public void DBSingletonReturnsDatabase()
		{
			DatabaseFactory.SetType<Database>();
			IDatabase db = DatabaseFactory.Factory();
			Assert.AreEqual(typeof(Database), db.GetType());
		}

		[TestMethod]
		public void DBSingletonChangesType()
		{
			DatabaseFactory.SetType<MockDB>();
			IDatabase db = DatabaseFactory.Factory();
			Assert.AreEqual(typeof(MockDB), db.GetType());

			DatabaseFactory.SetType<Database>();
			db = DatabaseFactory.Factory();
			Assert.AreEqual(typeof(Database), db.GetType());
		}
	}
}
