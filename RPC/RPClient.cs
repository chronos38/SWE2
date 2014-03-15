using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace RPC
{
	public class RPClient
	{
		TcpClient _client;
		NetworkStream _netStream;
		XmlSerializer _serializer;

		public RPClient()
		{
			_client = new TcpClient("127.0.0.1", 12345);
			_netStream = _client.GetStream();
			_serializer = new XmlSerializer(typeof(RPCall));
		}
		
		public void Send(RPCall procedure)
		{
			if (_netStream.CanWrite)
			{
				MemoryStream mem = new MemoryStream();
				_serializer.Serialize(mem, procedure);

				byte[] message = mem.GetBuffer();
				byte[] lengthPrefix = BitConverter.GetBytes(message.Length);
				byte[] buffer = new byte[message.Length + lengthPrefix.Length];
				lengthPrefix.CopyTo(buffer, 0);
				message.CopyTo(buffer, lengthPrefix.Length);
				_netStream.Write(buffer, 0, buffer.Length);
			}
		}

	}
}
