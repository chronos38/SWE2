using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;
using Server.DAL;

namespace Tests.MockDAL
{
	public class MockDB : IDatabase
	{
		public void Connect(string ip, int port, string user, string password, string db)
		{

		}
		public bool IsConnected()
		{
			return true;
		}
		public void Close()
		{
		}
		public List<Contact> SearchContacts(string filter)
		{
			List<Contact> result = new List<Contact>();
			Contact content;
			DateTime? time;
			switch (filter) 
			{
				case "Max":
					time = new DateTime?(new DateTime(1988,8,1));
					content = new Contact(2, null, "Mustermann", null, "Max", "Mustermann", null, time, "Musterstrasse", "1", "54321", "Musterstadt");
					break;
				case "null":
					return result;
				default:
					time = new DateTime?(new DateTime(2000,1,1));
					content = new Contact(1, "1", "teststring", "teststring", "teststring", "teststring", "teststring", time, "teststring", "teststring", "teststring", "teststring");
					break;
			}
			result.Add(content);
			return result;
		}
		public List<Invoice> SearchInvoices(string filter)
		{
			throw new NotImplementedException();
		}
		public void UpsertContact(Contact contact)
		{

		}
	}
}
