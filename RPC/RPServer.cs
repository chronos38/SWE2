using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RPC
{
	public class RPServer : IDisposable
	{
		TcpListener _server;
		IPAddress _ip;
		Thread _thread;
		int _port;

		Queue _queue = new Queue();


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

		public void Dispose()
		{
			_server.Stop();
			_thread.Abort();
		}

		public Queue RPCallQueue
		{
			get { return _queue; }
		}
	}
}
