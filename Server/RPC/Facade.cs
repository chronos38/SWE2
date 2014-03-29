using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Serialization;
using Server.BusinessLayer;
using Server.BusinessLayer.Commands;
using DataTransfer;

namespace Server.RPC
{
	public class Facade
	{

		XmlSerializer _RPCallSerializer = new XmlSerializer(typeof(RPCall));
		XmlSerializer _RPResultSerializer = new XmlSerializer(typeof(RPResult));

		public Facade()
		{

		}

		/// <summary>
		/// Handles incoming requests.
		/// </summary>
		/// <param name="ctx">The HttpListenerContext returned by the HttpListener</param>
		public void HandleConnection(HttpListenerContext ctx)
		{
			Debug.Assert(ctx != null);

			try {

				HttpListenerRequest req = ctx.Request;
				HttpListenerResponse res = ctx.Response;

				StreamReader msg = new StreamReader(req.InputStream);

				try{
					RPCall clientCall = Deserialize(msg);
					ICommand requestedCommand;
					if (!CommandDictionary.Instance.GetCommand(clientCall.procedureName, out requestedCommand)) {
						// Wahrscheinlich besser eine eigene Exception zu schreiben und 404 zurückgeben.
						throw new InvalidOperationException(); 
					}
					RPResult result = requestedCommand.Execute(clientCall);
					byte[] buffer = Encoding.UTF8.GetBytes(Serialize(result));
					Stream output = res.OutputStream;
					output.Write(buffer, 0, buffer.Length);
					res.Close();
				} catch(Exception ex) {
					// Deserialization did not work aborting.
					if (ex is InvalidOperationException || ex is ArgumentNullException) {
						Console.WriteLine(ex.Message);
						res.StatusCode = 505;
						res.Close();
						return;
					}
					throw;
				}
			} catch (HttpListenerException ex) {
				// Can't really do anything at this point.
				Console.WriteLine(ex.Message);
				return;
			}
		}

		RPCall Deserialize(StreamReader reader)
		{
			RPCall call = (RPCall)_RPCallSerializer.Deserialize(reader);
			return call;
		}

		string Serialize(RPResult result)
		{
			StringWriter writer = new StringWriter();
			_RPResultSerializer.Serialize(writer, result);
			return writer.ToString();
		}
	}
}
