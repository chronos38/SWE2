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
	class Facade
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
		public void HandleConnection(IHttpConnection con)
		{
			try {

				StreamReader msg = con.GetClientData();

				try{
					RPCall clientCall = Deserialize(msg);
					ICommand requestedCommand;
					if (!CommandDictionary.Instance.GetCommand(clientCall.procedureName, out requestedCommand)) {
						// Wahrscheinlich besser eine eigene Exception zu schreiben und 404 zurückgeben.
						throw new InvalidOperationException(); 
					}
					RPResult result = requestedCommand.Execute(clientCall);
					byte[] buffer = Encoding.UTF8.GetBytes(Serialize(result));
					con.SendReponseData(buffer);
				} catch(Exception ex) {
					// Deserialization did not work aborting.
					if (ex is InvalidOperationException || ex is ArgumentNullException) {
						Console.WriteLine(ex.Message);
						con.SendErrorCode(500);
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
