using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.BusinessLayer;
using Tests.Server.Commands;
using Server.BusinessLayer.Commands;

namespace Tests
{
	[TestClass]
	public class CommandDictionaryTests
	{
		CommandDictionary _dic = null;
		[TestInitialize]
		[TestMethod]
		public void CommandDictionaryIsInstanceable ()
		{
			_dic = CommandDictionary.Instance;
			Assert.IsNotNull(_dic);
			Assert.IsInstanceOfType(_dic, typeof(CommandDictionary));
		}

		[TestMethod]
		public void CommandDictionaryIsSingleton()
		{
			Assert.AreSame(_dic, CommandDictionary.Instance);
		}

		[TestMethod]
		public void RegisteredCommandIsRetrievable()
		{
			CommandTest com = new CommandTest();
			_dic.RegisterCommand("CommandTest", com);
			ICommand retCom = null;
			_dic.GetCommand("CommandTest", out retCom);
			Assert.AreEqual(com, retCom);
		}
	}
}
