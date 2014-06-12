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
		/// Handles incoming requests and calls the requested Command.
		/// Sends Error 500 on Error
		/// </summary>
		/// <param name="con">IHttpConnection Object wich wraps HttpListenerContext</param>
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

		/// <summary>
		/// Creates a RPCall Object from a Serialized XML.
		/// </summary>
		/// <param name="reader">The Serialized XML Object</param>
		/// <returns>RPCall Object containing the Clients Data</returns>
		RPCall Deserialize(StreamReader reader)
		{
			RPCall call = (RPCall)_RPCallSerializer.Deserialize(reader);
			return call;
		}

		/// <summary>
		/// Serializes any RPResult Object into a XML String
		/// </summary>
		/// <param name="result">Serialized XML Object</param>
		/// <returns>As XML Serialized Object</returns>
		string Serialize(RPResult result)
		{
			StringWriter writer = new StringWriter();
			_RPResultSerializer.Serialize(writer, result);
			return writer.ToString();
		}
	}
}
