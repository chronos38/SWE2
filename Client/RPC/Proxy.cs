using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using DataTransfer;
using DataTransfer.Types;
using System.Xml.Serialization;
using System.Data;
using DataTransfer.Converter;
using Client.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client.RPC
{
	public class Proxy
	{
		IRPClient _client;
		public Proxy(IRPClient client)
		{
			_client = client;
		}

		public Proxy()
		{
			_client =  new RPClient();
		}

		/// <summary>
		/// Search for a specific contact.
		/// </summary>
		/// <param name="name">The contacts name</param>
		/// <returns></returns>
		public async Task<RPResult> SearchContactsAsync(string name)
		{
			if (String.IsNullOrEmpty(name)) {
				throw new ArgumentNullException();
			}
			RPCall call = new RPCall("CommandContact");
			call.procedureArgs = new string[] { name };
			return await _client.SendAndReceiveAsync(call);
		}

		/// <summary>
		/// Send a Contact object to be updated or Inserted 
		/// into the database if it does not already exist.
		/// </summary>
		/// <param name="contact">The contact to be inserted or updated</param>
		/// <returns>
		///		A RPResult object its success field determines
		///		wether the query was successful(1) or not(0).
		/// </returns>
		public async Task<RPResult> SendContactAsync(Contact contact)
		{
			if (contact == null) {
				throw new ArgumentNullException();
			}
			RPCall call = new RPCall("CommandUpsert");
			List<Contact> temp = new List<Contact>() { contact };
			ContactListConverter conv = new ContactListConverter();
			call.dt = (DataTable) conv.ConvertTo(null, null, temp, typeof(DataTable));

			return await _client.SendAndReceiveAsync(call);

		}

		/// <summary>
		/// Returns all available companies 
		/// </summary>
		/// <returns>A RPResult with the companies in its "dt" field</returns>
		public async Task<RPResult> GetCompaniesAsync()
		{
			RPCall call = new RPCall("CommandGetCompanies");
			return await _client.SendAndReceiveAsync(call);

		}

		public async Task<RPResult> SearchInvoicesAsync(InvoiceSearchData data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, data);

			RPCall call = new RPCall("CommandInvoice", memoryStream.GetBuffer());
			return await _client.SendAndReceiveAsync(call);
		}

		public async Task<RPResult> SearchContactInvoicesAsync(int id)
		{
			RPCall call = new RPCall("CommandInvoice", new string[] { id.ToString() });
			return await _client.SendAndReceiveAsync(call);
		}

		internal async Task<RPResult> DeleteCompany(int p)
		{
			RPCall call = new RPCall("CommandDeleteCompany", new string[] { p.ToString() });
			return await _client.SendAndReceiveAsync(call);
		}

		internal async Task<RPResult> SearchCompany(int p, string company)
		{
			RPCall call = new RPCall("CommandSearchCompany", new string[] { p.ToString(), company });
			return await _client.SendAndReceiveAsync(call);
		}

		internal async Task<RPResult> SetCompany(int? id)
		{
			RPCall call = new RPCall("CommandSetCompany", new string[] { id.Value.ToString() });
			return await _client.SendAndReceiveAsync(call);
		}

		internal async Task<RPResult> UpsertInvoice(Invoice invoice)
		{
			RPCall call = new RPCall("CommandUpsertInvoice");
			call.dt = Invoice.CreateTable();
			call.dt.Rows.Add(invoice.ToDataRow(call.dt));
			return await _client.SendAndReceiveAsync(call);
		}

		internal async Task<RPResult> DeleteInvoiceItem(int p)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			RPCall call = new RPCall("CommandDeleteInvoiceItem");
			call.Buffer = BitConverter.GetBytes(p);
			return await _client.SendAndReceiveAsync(call);
		}
	}
}
