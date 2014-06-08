using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;
using Client.Data;
using System.Data;

namespace Server.DAL
{
	public interface IDatabase
	{
		void Connect(string ip, int port, string user, string password, string db);
		bool IsConnected();
		void Close();
		List<Contact> SearchContacts(string filter);
		List<Invoice> SearchInvoices(int id, DateTime? from, DateTime? to);
		List<Invoice> SearchInvoices(InvoiceSearchData data);
		List<Contact> GetCompanies();
		void UpsertContact(Contact contact);
		DataTable SearchCompany(int p1, string p2);
		int DeleteCompany(int p);
		DataTable GetCompany(int p);
		int UpsertInvoice(DataTable dataTable);
		int DeleteInvoiceItem(int p);
	}
}
