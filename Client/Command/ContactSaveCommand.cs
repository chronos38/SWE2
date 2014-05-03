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
	internal class ContactSaveCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			// TODO: implement save logic
			MessageBox.Show("Should save entry", "Save", MessageBoxButton.OK);
		}

		public ContactSaveCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
