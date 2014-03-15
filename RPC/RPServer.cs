using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RPC
{
	public class RPServer
	{
		TcpListener _server;
		IPAddress _ip;
		Thread _thread;
		int _port;

		Queue _queue;
		

		public RPServer()
		{
			_ip = IPAddress.Parse("127.0.0.1");
			_port = 12345;
			StartServer();
		}

		public RPServer(IPAddress ip, int port)
		{
			_ip = ip;
			_port = port;
			StartServer();
		}

		void StartServer()
		{
			_server = new TcpListener(_ip, _port);
			_server.Start();
			_thread = new Thread(Listen);
			_thread.IsBackground = true;
			_thread.Start();
		}

		void Listen()
		{
			try
			{
				while (true)
				{
					TcpClient tcpClient = _server.AcceptTcpClient();
					RPHandler stub = new RPHandler(this, tcpClient);
				}
			}
			catch(SocketException ex)
			{
				Console.WriteLine("SocketException: {0}", ex);
			}
		}

		public void Close()
		{
			_thread.Abort();
			_server.Stop();
		}

		public Queue RPCallQueue
		{
			get { return _queue; }
		}
	}
}
