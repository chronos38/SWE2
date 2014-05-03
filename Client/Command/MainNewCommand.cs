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
	class MainNewCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			EditWindow window = new EditWindow();
			window.ShowDialog();
		}

		public MainNewCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
