using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Command
{
	internal abstract class Command : ICommand
	{
		public event EventHandler CanExecuteChanged;
		public abstract bool CanExecute(object parameter);
		public abstract void Execute(object parameter);
		protected Window Window { get; set; }
		protected ViewModel.ViewModel Model { get; set; }
	}
}
