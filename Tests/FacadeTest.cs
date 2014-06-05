using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.BusinessLayer;
using Server.RPC;
using Tests.Server.Commands;
using Tests.Server.RPC;
using DataTransfer;
using System.Xml.Serialization;
using System.IO;

namespace Tests
{
	[TestClass]
	public class FacadeTest
	{
		[TestInitialize]
		public void Setup()
		{
			CommandDictionary.Instance.RegisterCommand("CommandTest", new CommandTest());
		}

		[TestMethod]
		public void FacadeHandleConnectionExecutesExistingCommand()
		{
			MockHttpConnection con = new MockHttpConnection();
			con.CommandName = "CommandTest";

			Facade fc = new Facade();
			fc.HandleConnection(con);
			RPResult res = Deserialize(con.ResponseData);
			Assert.AreEqual(12345, res.count);
		}

		[TestMethod]
		public void FacadeHandleConnectionError500OnNonExistingCommand()
		{
			MockHttpConnection con = new MockHttpConnection();
			con.CommandName = "CommandDoesNotExist";

			Facade fc = new Facade();
			fc.HandleConnection(con);
			Assert.AreEqual(500, con.StatusCode);
		}

		private RPResult Deserialize(string res)
		{
			XmlSerializer RPResultSerializer = new XmlSerializer(typeof(RPResult));
			StringReader reader = new StringReader(res);
			RPResult ret = (RPResult)RPResultSerializer.Deserialize(reader);
			return ret;
		}
	}
}
