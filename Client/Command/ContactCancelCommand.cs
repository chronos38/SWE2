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
	internal class ContactCancelCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			Window.Close();
		}

		public ContactCancelCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
