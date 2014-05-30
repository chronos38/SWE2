using Client.ViewModel;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Command
{
	internal class MainOpenCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			DataRowView drv = parameter as DataRowView;

			if (drv != null) {
				SearchViewModel model = Model as SearchViewModel;
				EditWindow window = new EditWindow(new Contact(drv.Row.ItemArray));
				window.ShowDialog();
				model.SearchResult = null;
			}
		}

		public MainOpenCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
