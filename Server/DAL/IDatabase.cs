using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;
using Client.Data;

namespace Server.DAL
{
	public interface IDatabase
	{
		void Connect(string ip, int port, string user, string password, string db);
		bool IsConnected();
		void Close();
		List<Contact> SearchContacts(string filter);
		List<Invoice> SearchInvoices(int id);
		List<Invoice> SearchInvoices(InvoiceSearchData data);
		List<Contact> GetCompanies();
		void UpsertContact(Contact contact);
	}
}
