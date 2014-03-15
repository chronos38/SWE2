using System;
using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;
using RPC;

namespace GUI
{
	class RPClient
	{
		TcpClient _client;
		NetworkStream _netStream;
		XmlSerializer _ser;

		public RPClient()
		{
			_client = new TcpClient("127.0.0.1", 12345);
			_netStream = _client.GetStream();
			_ser = new XmlSerializer(typeof(RP))
		}
		
		public void Send()
		{

		}

	}
}
