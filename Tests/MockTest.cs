using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using Server.DAL;
using DataTransfer.Types;
using System.Collections.Generic;
using Tests.MockDAL;

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

			/*List<Contact> test = db.SearchContacts("Max");
			Assert.AreEqual(test[0].Forename, "Max");
			Assert.AreEqual(test[0].Surname, "Mustermann");*/
		}

		[TestMethod]
		public void DBSingletonReturnsDatabase()
		{
			IDatabase db = IDatabaseSingleton.Instance();
			Assert.AreEqual(typeof(Database), db.GetType());
		}
	}
}
