using System;
using System.IO;
using System.Net.Sockets;
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
				_serializer.Serialize(_netStream, procedure);
			}
		}

	}
}
