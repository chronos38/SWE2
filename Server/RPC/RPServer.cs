using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Server.RPC
{
	public class RPServer : IDisposable
	{
		HttpListener _listener;
		Semaphore _sem;
		int _port;
		readonly int _accepts;
		Facade _facade = new Facade();
		/// <summary>
		/// Creates an asyncronous HTTP server
		/// </summary>
		/// <param name="port">The port the server listens on</param>
		/// <param name="accepts">
		/// Higher numbers mean more connections can be maintained but at a slower response time.
		/// Lower numbers mean less connections can be maintained but at a faster response time.
		/// </param>
		public RPServer(int port, int accepts)
		{
			_port = port;
			_accepts = accepts * Environment.ProcessorCount;
			_listener = new HttpListener();
			_listener.Prefixes.Add(String.Format("http://localhost:{0}/", _port));
		}
		/// <summary>
		/// Starts the server.
		/// This method will block.
		/// </summary>
		public void Run()
		{
			try {
				_listener.Start();
			} catch (HttpListenerException ex) {
				Console.WriteLine(ex.Message);
				Console.WriteLine("Try running the following command: netsh http add urlacl url=http://localhost:<port>/ user=YOUR_USERNAME listen=yes");
				return;
			}
			
			// Create Semaphore according to the number of accepted Connections.
			_sem = new Semaphore(_accepts, _accepts);

			while (true) {
				// Block if maximum number of concurrent threads is reached.
				_sem.WaitOne();
				_listener.GetContextAsync().ContinueWith(async (ctxTask) => {
					string error;
					try {
						_sem.Release(); // At this point a new listener thread can be created.
						HttpListenerContext ctx = await ctxTask;
						HttpConnection con = new HttpConnection(ctx);
						_facade.HandleConnection(con);
						return;
					} catch (Exception ex) {
						error = ex.ToString();
					}
					await Console.Error.WriteLineAsync(error);
				});
			}
		}

		public void Dispose()
		{
			_listener.Close();
		}
	}
}
