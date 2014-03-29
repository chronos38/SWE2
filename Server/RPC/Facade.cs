using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Serialization;
using Server.BusinessLayer.Commands;
using DataTransfer;

namespace Server.RPC
{
	public class Facade
	{
		static ConcurrentDictionary<string, ICommand> _availableCommands = new ConcurrentDictionary<string, ICommand>();

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
					_availableCommands.TryGetValue(clientCall.procedureName, out requestedCommand);
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

		/// <summary>
		/// Register a new Command to be executed via RPC.
		/// </summary>
		/// <param name="commandName">The name of the method</param>
		/// <param name="command">The instance of the command to be executed</param>
		static public void RegisterCommand(string commandName, ICommand command)
		{
			try {
				_availableCommands.TryAdd(commandName, command);
			} catch (ArgumentNullException ex) {
				Console.WriteLine(ex.Message);
			} catch (OverflowException ex) {
				Console.WriteLine(ex.Message);
			}
		}
	}
}
