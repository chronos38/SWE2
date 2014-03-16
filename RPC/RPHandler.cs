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
		PacketProtocol _pp;
		public RPHandler(RPServer server, TcpClient client)
		{
			_server = server;
			_client = client;
			_pp = new PacketProtocol(_client.ReceiveBufferSize);
			_netStream = _client.GetStream();

			_serializer = new XmlSerializer(typeof(RPCall));
			_thread = new Thread(Receive);
			_thread.IsBackground = true;
			_thread.Start();
		}

		void Receive()
		{
			while (_client.Connected)
			{
				if (_pp.Messages.Count == 0)
				{
					byte[] buffer = new byte[_client.ReceiveBufferSize];
					int received = _netStream.Read(buffer, 0, _client.ReceiveBufferSize);
					_pp.DataReceived(buffer);
				} 
				else
				{
					DeserializeMessage();
				}
			}

		}

		void DeserializeMessage()
		{
			byte[] message = (byte[])_pp.Messages.Dequeue();
			MemoryStream mem = new MemoryStream(message, 0, message.Length);
			object result = _serializer.Deserialize(mem);
			_server.RPCallQueue.Enqueue(result);
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
