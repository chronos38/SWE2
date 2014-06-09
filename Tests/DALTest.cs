using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DAL;
using System.IO;

namespace Tests
{
	[TestClass]
	public class DALTest
	{
		Database _TestDB = null;

		[TestInitialize]
		public void SetUp()
		{
			//Testdb muss noch händisch erstellt werden.
			_TestDB = new Database();
			_TestDB.Connect("127.0.0.1", 5432, "testadmin", "test", "testdb");
			string pathTables = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server/TestDBSkripts","2.CreateTable.sql");
			string pathEntries = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server/TestDBSkripts","3.CreateEntries.sql");
			_TestDB.ExecuteSqlScript(pathTables);
			_TestDB.ExecuteSqlScript(pathEntries);
		}

		[TestMethod]
		public void DatabaseIsIstanceable()
		{
			Database db = null;
			db = new Database();
			Assert.IsNotNull(db);
		}

		[TestMethod]
		public void DatabaseConnectsToServer()
		{
			Database db = new Database();
			db.Connect("127.0.0.1", 5432, "testadmin", "test", "testdb");
			Assert.IsTrue(db.IsConnected());
		}

		[TestMethod]
		public void DatabaseClosesConnection()
		{
			Database db = new Database();
			db.Connect("127.0.0.1", 5432, "testadmin", "test", "testdb");
			db.Close();
			Assert.IsFalse(db.IsConnected());
		}

	}
}
