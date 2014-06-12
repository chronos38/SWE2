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

		/// <param name="ctx">HttpListenerContext returned by HttpListener</param>
		public HttpConnection(HttpListenerContext ctx)
		{
			_ctx = ctx;
		}

		/// <returns>Returns StreamReader Object containing the Data sent by the Client.</returns>
		public StreamReader GetClientData()
		{
			return new StreamReader(_ctx.Request.InputStream);
		}

		/// <summary>
		/// Send Data to the client and closes the connection
		/// </summary>
		/// <param name="response">byte array containing all data to be sent</param>
		public void SendReponseData(byte[] response)
		{
			_ctx.Response.OutputStream.Write(response, 0, response.Length);
			_ctx.Response.Close();
		}

		/// <summary>
		/// Sets the Http Error Code.
		/// </summary>
		/// <param name="statusCode">Http Error Code</param>
		public void SendErrorCode(int statusCode)
		{
			_ctx.Response.StatusCode = statusCode;
		}
	}
}
