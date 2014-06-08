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
			EditViewModel model = Model as EditViewModel;
			EditInvoiceWindow window = new EditInvoiceWindow(model.ID);
			window.ShowDialog();
			model.CreateInvoiceTable();
		}

		public InvoiceNewCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
