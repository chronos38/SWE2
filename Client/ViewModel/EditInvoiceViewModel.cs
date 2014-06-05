using Client.Command;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
	internal class EditInvoiceViewModel : ViewModel
	{
		private EditInvoiceWindow Window { get; set; }
		public int ID { get; private set; }

		public EditInvoiceViewModel(EditInvoiceWindow window, Invoice invoice)
		{
			Window = window;
			CreateCommands();
			// TODO: alles

			DataTable table = new DataTable("InvoiceItems");
			table.Columns.Add("Name", typeof(string));
			table.Columns.Add("Quantity", typeof(int));
			table.Columns.Add("UnitPrice", typeof(double));
			table.Columns.Add("VAT", typeof(double));

			foreach (InvoiceItem item in invoice.Items) {
				DataRow row = table.NewRow();
				row["Name"] = item.Name;

				if (item.Quantity == null) {
					row["Quantity"] = DBNull.Value;
				} else {
					row["Quantity"] = item.Quantity;
				}

				if (item.UnitPrice == null) {
					row["UnitPrice"] = DBNull.Value;
				} else {
					row["UnitPrice"] = item.UnitPrice;
				}

				if (item.VAT == null) {
					row["VAT"] = DBNull.Value;
				} else {
					row["VAT"] = item.VAT;
				}

				table.Rows.Add(row);
			}

			table.AcceptChanges();
			InvoiceItems = table.DefaultView;
		}

		public EditInvoiceViewModel(EditInvoiceWindow window, int contact)
		{
			Window = window;
			CreateCommands();
			// TODO: alles
		}

		private DataView _invoiceItems = null;
		public DataView InvoiceItems
		{
			get { return _invoiceItems; }
			set
			{
				if (_invoiceItems != value) {
					_invoiceItems = value;
					OnPropertyChanged("InvoiceItems");
				}
			}
		}

		public ICommand NewInvoiceItem { get; private set; }
		public ICommand DeleteInvoiceItem { get; private set; }

		private void CreateCommands()
		{
			NewInvoiceItem = new InvoiceItemNewCommand(Window, this);
			DeleteInvoiceItem = new InvoiceItemDeleteCommand(Window, this);
		}
	}
}
