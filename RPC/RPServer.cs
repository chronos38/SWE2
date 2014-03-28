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
using System.Xml.Serialization;


namespace RPC
{
	public class RPServer : IDisposable
	{
		HttpListener _listener;
		Semaphore _sem;
		int _port;
		readonly int _accepts;
		XmlSerializer _serializer;

		public RPServer(int port, int accepts)
		{
			_port = port;
			_accepts = accepts * Environment.ProcessorCount;
			_serializer = new XmlSerializer(typeof(RPResult));
			_listener = new HttpListener();
			_listener.Prefixes.Add(String.Format("http://localhost:{0}/", _port));
		}

		public void Run()
		{
			try {
				_listener.Start();
			} catch (HttpListenerException ex) {
				//netsh http add urlacl url=http://localhost:12345/ user=YOUR_USERNAME listen=yes
				Console.WriteLine(ex.Message);
				return;
			}

			_sem = new Semaphore(_accepts, _accepts);

			while (true) {
				_sem.WaitOne();
				_listener.GetContextAsync().ContinueWith(async (ctxTask) => {
					string error;
					try {
						_sem.Release();

						HttpListenerContext ctx = await ctxTask;
						HandleConnection(ctx);
						return;
					} catch (Exception ex) {
						error = ex.ToString();
					}
					await Console.Error.WriteLineAsync(error);
				});
			}
		}

		void HandleConnection(HttpListenerContext ctx)
		{
			Debug.Assert(ctx != null);

			try {
				HttpListenerRequest req = ctx.Request;
				HttpListenerResponse res = ctx.Response;
				res.StatusCode = 200;
				string postBody = new StreamReader(req.InputStream).ReadToEnd();

				byte[] buffer = Encoding.UTF8.GetBytes(postBody);
				Stream output = res.OutputStream;
				output.Write(buffer, 0, buffer.Length);
				output.Close();
				res.Close();
			} catch (HttpListenerException) {
				//dunno
			}
		}

		public void Dispose()
		{
			_listener.Stop();
		}
	}
}
