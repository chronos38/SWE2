using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Server.RPC
{
	class HttpConnection : IHttpConnection
	{
		HttpListenerContext _ctx;

		public HttpConnection(HttpListenerContext ctx)
		{
			_ctx = ctx;
		}

		public StreamReader GetClientData()
		{
			return new StreamReader(_ctx.Request.InputStream);
		}


		public void SendReponseData(byte[] response)
		{
			_ctx.Response.OutputStream.Write(response, 0, response.Length);
			_ctx.Response.Close();
		}

		public void SendErrorCode(int statusCode)
		{
			_ctx.Response.StatusCode = statusCode;
		}
	}
}
