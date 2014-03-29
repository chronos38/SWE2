using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.BusinessLayer.Commands;

namespace Server.BusinessLayer
{
	public class CommandDictionary
	{
		static readonly CommandDictionary _Instance = new CommandDictionary();
		ConcurrentDictionary<string, ICommand> _availableCommands = new ConcurrentDictionary<string, ICommand>();

		public static CommandDictionary Instance
		{
			get { return _Instance; }
		}

		public bool GetCommand(string commandName, out ICommand value)
		{
			bool retval = _availableCommands.TryGetValue(commandName, out value);
			return retval;
		}

		/// <summary>
		/// Register a new Command to be executed via RPC.
		/// </summary>
		/// <param name="commandName">The name of the method</param>
		/// <param name="command">The instance of the command to be executed</param>
		public void RegisterCommand(string commandName, ICommand command)
		{
			try {
				_availableCommands.TryAdd(commandName, command);
			} catch (ArgumentNullException ex) {
				Console.WriteLine(ex.Message);
			} catch (OverflowException ex) {
				Console.WriteLine(ex.Message);
			}
		}
		private CommandDictionary()
		{

		}
	}
}