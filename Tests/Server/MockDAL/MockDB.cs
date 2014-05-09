using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;
using Server.DAL;

namespace Tests.Server.MockDAL
{
	public class MockDB : IDatabase
	{

		TestHelper th = new TestHelper();

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
			switch (filter) 
			{
				case "Max":
					content = th.GetTestPersonContact();
					break;
				default:
					return result;
			}
			result.Add(content);
			return result;
		}
		public List<Invoice> SearchInvoices(string filter)
		{
			throw new NotImplementedException();
		}

		public List<Contact> GetCompanies()
		{
			List<Contact> result = new List<Contact>();
			result.Add(th.GetTestCompanyContact());
			return result;
		}
		public void UpsertContact(Contact contact)
		{

		}
	}
}
