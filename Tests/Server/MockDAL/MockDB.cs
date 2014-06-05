using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;
using Server.DAL;
using Client.Data;

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
		public List<Invoice> SearchInvoices(int id)
		{
			throw new NotImplementedException();
		}

		public List<Invoice> SearchInvoices(InvoiceSearchData data)
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

		public DataTable SearchCompany(int p1, string p2)
		{
			throw new NotImplementedException();
		}

		public int DeleteCompany(int p)
		{
			throw new NotImplementedException();
		}

		public DataTable SetCompany(int p)
		{
			throw new NotImplementedException();
		}
	}
}
