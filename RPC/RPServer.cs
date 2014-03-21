using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;


namespace RPC
{
	public class RPServer : IDisposable
	{
		HttpListener _listener;
		Semaphore _sem;
		int _port;
		readonly int _accepts;
		

		Queue _queue = new Queue();
		public string test;

		public RPServer(int port, int accepts)
		{
			_port = port;
			_accepts = accepts * Environment.ProcessorCount;

			_listener = new HttpListener();
			_listener.Prefixes.Add(String.Format("http://localhost:{0}/", _port));
		}

		public void Run()
		{
			try
			{
				_listener.Start();
			} 
			catch(HttpListenerException ex)
			{
				//netsh http add urlacl url=http://localhost:12345/ user=YOUR_USERNAME listen=yes
				Console.WriteLine(ex.Message);
				return;
			}

			 _sem = new Semaphore(_accepts, _accepts);

			while(true)
			{
				_sem.WaitOne();
				_listener.GetContextAsync().ContinueWith(async (ctxTask) =>
				{
					string error;
					try
					{
						_sem.Release();

						HttpListenerContext ctx = await ctxTask;
						ParseUrl(ctx);
						return;
					}
					catch(Exception ex)
					{
						error = ex.ToString();
					}
					await Console.Error.WriteLineAsync(error);
				});
			}
		}

		void ParseUrl(HttpListenerContext ctx)
		{
			Debug.Assert(ctx != null);

			try
			{
				HttpListenerRequest req = ctx.Request;
				HttpListenerResponse res = ctx.Response;
				test = HttpUtility.ParseQueryString(req.Url.Query).Get("function1");

			}
			catch(HttpListenerException)
			{
				//dunno
			}
		}

		public void Dispose()
		{
			_listener.Stop();
		}

		public Queue RPCallQueue
		{
			get { return _queue; }
		}
	}
}
