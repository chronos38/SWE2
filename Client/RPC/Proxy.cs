using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

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
	}
}
