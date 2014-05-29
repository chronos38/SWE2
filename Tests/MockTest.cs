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
			DatabaseSingleton.SetType<MockDB>();
			IDatabase db = DatabaseSingleton.Instance();
			Assert.AreEqual(typeof(MockDB), db.GetType());
		}

		[TestMethod]
		public void DBSingletonReturnsDatabase()
		{
			DatabaseSingleton.SetType<Database>();
			IDatabase db = DatabaseSingleton.Instance();
			Assert.AreEqual(typeof(Database), db.GetType());
		}

		[TestMethod]
		public void DBSingletonChangesType()
		{
			DatabaseSingleton.SetType<MockDB>();
			IDatabase db = DatabaseSingleton.Instance();
			Assert.AreEqual(typeof(MockDB), db.GetType());

			DatabaseSingleton.SetType<Database>();
			db = DatabaseSingleton.Instance();
			Assert.AreEqual(typeof(Database), db.GetType());
		}
	}
}
