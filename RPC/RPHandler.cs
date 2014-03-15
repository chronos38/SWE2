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
		byte[] _received;

		public RPHandler(RPServer server, TcpClient client)
		{
			_server = server;
			_client = client;
			_received = new byte[_client.ReceiveBufferSize];
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
				byte[] buffer = new byte[_client.ReceiveBufferSize];
				int received = _netStream.Read(buffer, 0, _client.ReceiveBufferSize);
				int messageLength = BitConverter.ToInt32(buffer, 0);
				MemoryStream mem = new MemoryStream(buffer, 4, messageLength);
				StreamReader reader = new StreamReader(mem);
				string test = reader.ReadToEnd();
				mem.Position = 0;
				object result = _serializer.Deserialize(mem);
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
