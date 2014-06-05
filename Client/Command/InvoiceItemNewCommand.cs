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
	class InvoiceItemNewCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			EditInvoiceViewModel model = Model as EditInvoiceViewModel;
			EditInvoiceWindow window = Window as EditInvoiceWindow;

			if (model != null) {
				DataTable table = model.InvoiceItems.ToTable();
				table.Rows.Add(table.NewRow());
				table.AcceptChanges();
				model.InvoiceItems = table.DefaultView;
			}
		}

		public InvoiceItemNewCommand(Window window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
