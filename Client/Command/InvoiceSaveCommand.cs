using Client.RPC;
using Client.ViewModel;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command
{
	class InvoiceSaveCommand : Command
	{
		public override void Execute(object parameter)
		{
			EditInvoiceViewModel model = Model as EditInvoiceViewModel;

			if (model != null) {
				Proxy proxy = new Proxy();
				DataTable table = model.InvoiceItems.Table;
				List<InvoiceItem> items = new List<InvoiceItem>();

				foreach (DataRow row in table.Rows) {
					items.Add(
						new InvoiceItem(
							row["Name"] as string,
							row["Quantity"] as int?,
							row["UnitPrice"] as double?,
							row["VAT"] as double?
						)
					);
				}

				Invoice invoice = new Invoice(
					model.ID,
					model.Name,
					model.Date,
					model.Maturity,
					model.Comment,
					model.Message,
					model.Type,
					items,
					model.Contact,
					model.IsReadOnly == true
				);

				proxy.UpsertInvoice(invoice);
			}
		}

		public InvoiceSaveCommand(EditInvoiceWindow window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
