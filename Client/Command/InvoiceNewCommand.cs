using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Command
{
	class InvoiceNewCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			throw new NotImplementedException();
		}

		public InvoiceNewCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
