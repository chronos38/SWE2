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

		public async Task GetContactsAsync()
		{
			RPCall call = new RPCall("CommandContact");
			RPResult result = await _client.SendAndReceiveAsync(call);
			Console.WriteLine(result.dt.Rows[0].ItemArray[0]);
		}
	}
}
