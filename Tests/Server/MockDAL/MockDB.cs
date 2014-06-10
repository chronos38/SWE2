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

		TestHelper _th = new TestHelper();

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
					content = _th.GetTestPersonContact();
					break;
				default:
					return result;
			}
			result.Add(content);
			return result;
		}
		public List<Invoice> SearchInvoices(int id, DateTime? from, DateTime? to)
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
			result.Add(_th.GetTestCompanyContact());
			return result;
		}
		public void UpsertContact(Contact contact)
		{

		}

		public DataTable SearchCompany(int p1, string p2)
		{
			if ((p1 == 1) && (p2 == "teststring")) {
				return _th.GetTestCompanyDataTable();
			} else {
				return new DataTable();
			}
		}

		public int DeleteCompany(int p)
		{
			return 1;
		}

		public DataTable GetCompany(int p)
		{
			if (p == 1) {
				return _th.GetTestCompanyDataTable();
			} else {
				return new DataTable();
			}
		}
		
		public int UpsertInvoice(DataTable dataTable)
		{
			throw new NotImplementedException();
		}

		public int DeleteInvoiceItem(int p)
		{
			throw new NotImplementedException();
		}

		public List<Invoice> SelectInvoice(int p)
		{
			throw new NotImplementedException();
		}
	}
}
