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
		private int Contact { get; set; }

		public EditInvoiceViewModel(EditInvoiceWindow window, Invoice invoice)
		{
			Window = window;
			InitializeComponents();

			// Properties
			ID = invoice.ID;
			Name = invoice.Name;
			Date = invoice.Date;
			Maturity = invoice.Maturity;
			Comment = invoice.Comment;
			Message = invoice.Message;
			Type = invoice.Type;
			IsReadOnly = invoice.ReadOnly;
			Contact = invoice.Contact;

			// Invoice items
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
			InitializeComponents();
			// TODO: alles
		}

		private string _name = null;
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value) {
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}

		private DateTime? _date = null;
		public DateTime? Date
		{
			get { return _date; }
			set
			{
				if (_date != value) {
					_date = value;
					OnPropertyChanged("Date");
				}
			}
		}

		private DateTime? _maturity = null;
		public DateTime? Maturity
		{
			get { return _maturity; }
			set
			{
				if (_maturity != value) {
					_maturity = value;
					OnPropertyChanged("Maturity");
				}
			}
		}

		private string _comment = null;
		public string Comment
		{
			get { return _comment; }
			set
			{
				if (_comment != value) {
					_comment = value;
					OnPropertyChanged("Comment");
				}
			}
		}

		private string _message = null;
		public string Message
		{
			get { return _message; }
			set
			{
				if (_message != value) {
					_message = value;
					OnPropertyChanged("Message");
				}
			}
		}

		private string _type = null;
		public string Type
		{
			get { return _type; }
			set
			{
				if (_type != value) {
					_type = value;
					OnPropertyChanged("Type");
				}
			}
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

		private bool? _isReadOnly = null;
		public bool? IsReadOnly
		{
			get { return _isReadOnly; }
			set
			{
				if (_isReadOnly != value) {
					_isReadOnly = value;
					OnPropertyChanged("IsReadOnly");
					OnPropertyChanged("IsWritable");
				}
			}
		}
		public bool? IsWritable
		{
			get { return !_isReadOnly; }
		}

		public ICommand NewInvoiceItem { get; private set; }
		public ICommand DeleteInvoiceItem { get; private set; }
		public ICommand Cancel { get; private set; }
		public ICommand Save { get; private set; }
		public ICommand Print { get; private set; }

		private void InitializeComponents()
		{
			// ComboBox
			Window.cmbType.Items.Add("Incoming");
			Window.cmbType.Items.Add("Outgoing");

			// Commands
			NewInvoiceItem = new InvoiceItemNewCommand(Window, this);
			DeleteInvoiceItem = new InvoiceItemDeleteCommand(Window, this);
			Cancel = new InvoiceCancelCommand(Window, this);
			Save = new InvoiceSaveCommand(Window, this);
			Print = new InvoicePrintCommand(Window, this);
		}
	}
}
