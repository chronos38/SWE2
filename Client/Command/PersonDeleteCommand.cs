using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Command
{
	internal class PersonDeleteCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
		}

		public PersonDeleteCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
