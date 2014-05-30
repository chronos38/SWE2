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
			RPResult result = await _client.SendAndReceiveAsync(call);
			return result;
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

			RPResult result = await _client.SendAndReceiveAsync(call);
			return result;

		}

		/// <summary>
		/// Returns all available companies 
		/// </summary>
		/// <returns>A RPResult with the companies in its "dt" field</returns>
		public async Task<RPResult> GetCompaniesAsync()
		{
			RPCall call = new RPCall("CommandGetCompanies");
			RPResult result = await _client.SendAndReceiveAsync(call);
			return result;

		}

		public async Task<RPResult> SearchInvoicesAsync(InvoiceSearchData data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, data);

			RPCall call = new RPCall("CommandInvoice", memoryStream.GetBuffer());
			RPResult result = await _client.SendAndReceiveAsync(call);
			return result;
		}

		public async Task<RPResult> SearchContactInvoicesAsync(int id)
		{
			RPCall call = new RPCall("CommandInvoice", new string[] { id.ToString() });
			call.Buffer = null;
			RPResult result = await _client.SendAndReceiveAsync(call);
			return result;
		}
	}
}
