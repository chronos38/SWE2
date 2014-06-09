using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command
{
	class InvoicePrintCommand : Command
	{
		public async override void Execute(object parameter)
		{
			EditInvoiceViewModel model = Model as EditInvoiceViewModel;

			if (model != null) {
				Proxy proxy = new Proxy();
				DataTable table = model.InvoiceItems.Table;
				List<InvoiceItem> items = new List<InvoiceItem>();

				foreach (DataRow row in table.Rows) {
					int? id = row["ID"] as int?;

					items.Add(
						new InvoiceItem(
							id == null ? -1 : id.Value,
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

				await proxy.UpsertInvoice(invoice);
				RPResult result = await proxy.SelectInvoice(model.ID);
				List<Invoice> invoices = null;
				table = result.dt;

				foreach (DataRow row in table.Rows) {
					invoices.Add(new Invoice(row));
				}

				if (invoices.Count != 1) {
					throw new Exception("Motherfucker!");
				}

				// TODO: PDF erstellen
			}
		}

		public InvoicePrintCommand(EditInvoiceWindow window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
