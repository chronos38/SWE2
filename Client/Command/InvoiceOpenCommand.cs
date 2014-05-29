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
	class InvoiceOpenCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			DataRowView drv = parameter as DataRowView;

			if (drv != null) {
				EditInvoiceWindow window = new EditInvoiceWindow(new Invoice(drv.Row.ItemArray));
				window.ShowDialog();
			}
		}

		public InvoiceOpenCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
