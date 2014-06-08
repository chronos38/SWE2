using Client.RPC;
using Client.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Command
{
	class InvoiceItemDeleteCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public async override void Execute(object parameter)
		{
			EditInvoiceViewModel model = Model as EditInvoiceViewModel;
			EditInvoiceWindow window = Window as EditInvoiceWindow;

			if (model != null && window != null) {
				Proxy proxy = new Proxy();
				DataTable table = model.InvoiceItems.Table;
				int count = window.dgrdInvoiceItems.SelectedItems.Count;
				object[] items = new object[count];
				window.dgrdInvoiceItems.SelectedItems.CopyTo(items, 0);

				foreach (object item in items) {
					DataRowView rowView = item as DataRowView;
					int? id = rowView.Row["ID"] as int?;
					table.Rows.Remove(rowView.Row);

					if (id != null) {
						await proxy.DeleteInvoiceItem(id.Value);
					}
				}
			}
		}

		public InvoiceItemDeleteCommand(Window window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
