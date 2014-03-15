using System;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace RPC
{
	class RPHandler
	{

		RPServer _server;
		NetworkStream _netStream;
		TcpClient _client;
		XmlSerializer _serializer;
		Thread _thread;

		public RPHandler(RPServer server, TcpClient client)
		{
			_server = server;
			_client = client;
			_netStream = _client.GetStream();

			_serializer = new XmlSerializer(typeof(RPCall));
			_thread = new Thread(Receive);
			_thread.IsBackground = true;
			_thread.Start();
		}

		void Receive()
		{
			while(_client.Connected)
			{
				StreamReader reader = new StreamReader(_netStream);
				 object result = _serializer.Deserialize(reader);
				 Thread.Sleep(2000);
				_server.RPCallQueue.Enqueue(result);
			}

		}
		
		public void Send(int id, RPResult result)
		{
			_serializer.Serialize(_netStream, result);
		}

		public void Close()
		{
			_netStream.Dispose();
			_thread.Abort();
		}
	}
}
