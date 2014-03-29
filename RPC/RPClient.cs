using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace DataTransfer
{
	public class RPClient : IDisposable
	{
		HttpClient _client;
		XmlSerializer _serializer;

		public RPClient()
		{
			_client = new HttpClient();
			_serializer = new XmlSerializer(typeof(RPCall));
		}

		public async Task SendAsync(RPCall call)
		{
			StringWriter textWriter = new StringWriter();
			_serializer.Serialize(textWriter, call);
			HttpResponseMessage response = await _client.PostAsync("http://localhost:12345/", new StringContent(textWriter.ToString()));
			response.EnsureSuccessStatusCode();
			string result = await response.Content.ReadAsStringAsync();
			Console.WriteLine(result);
		}

		public void Dispose()
		{
			_client.Dispose();
		}

	}
}
