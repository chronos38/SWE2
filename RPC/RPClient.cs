using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace RPC
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
		
		public async void Send()
		{
			HttpResponseMessage response = await _client.GetAsync("http://localhost:12345/?function1=testargument");
			/*MemoryStream mem = new MemoryStream();
			_serializer.Serialize(mem, procedure);
			byte[] buffer = PacketProtocol.WrapMessage(mem.GetBuffer());
			_netStream.Write(buffer, 0, buffer.Length);*/
		}

		public void Dispose()
		{
		}

	}
}
