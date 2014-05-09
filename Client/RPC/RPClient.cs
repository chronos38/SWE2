using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using DataTransfer;

namespace Client.RPC
{
	public class RPClient : IDisposable, IRPClient
	{
		HttpClient _client = new HttpClient();
		XmlSerializer _RPCallSerializer = new XmlSerializer(typeof(RPCall));
		XmlSerializer _RPResultSerializer = new XmlSerializer(typeof(RPResult));

		public RPClient()
		{
		}

		public async Task<RPResult> SendAndReceiveAsync(RPCall call)
		{
			StringContent sendCall = new StringContent(Serialize(call).ToString());
			HttpResponseMessage response = await _client.PostAsync("http://localhost:12345/", sendCall);
			response.EnsureSuccessStatusCode();
			string resultString = await response.Content.ReadAsStringAsync();
			RPResult result = Deserialize(resultString);
			return result;
		}

		public void Dispose()
		{
			_client.Dispose();
		}

		RPResult Deserialize(string resultString)
		{
				StringReader reader = new StringReader(resultString);
				RPResult retVal = (RPResult) _RPResultSerializer.Deserialize(reader);
				return retVal;
		}

		string Serialize(RPCall call)
		{
			StringWriter writer = new StringWriter();
			_RPCallSerializer.Serialize(writer, call);
			return writer.ToString();
		}

	}
}
