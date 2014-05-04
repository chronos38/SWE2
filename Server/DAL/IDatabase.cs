using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Types;

namespace Server.DAL
{
	public interface IDatabase
	{
		void Connect(string ip, int port, string user, string password, string db);
		bool IsConnected();
		void Close();
		List<Contact> SearchContacts(string filter);
		List<Invoice> SearchInvoices(string filter);
		void UpsertContact(Contact contact);
	}
}
