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

namespace Client.RPC
{
	public class Proxy
	{
		RPClient _client = new RPClient();
		public Proxy()
		{

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

		public async Task<RPResult> GetCompaniesAsync()
		{
			RPCall call = new RPCall("CommandGetCompanies");
			RPResult result = await _client.SendAndReceiveAsync(call);
			return result;

		}
	}
}
