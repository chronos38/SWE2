using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
	internal class EditInvoiceViewModel : ViewModel
	{
		private EditInvoiceWindow Window { get; set; }
		public int ID { get; private set; }

		public EditInvoiceViewModel(EditInvoiceWindow window, Invoice invoice)
		{
			// TODO: alles
			Window = window;
		}

		public EditInvoiceViewModel(EditInvoiceWindow window, int contact)
		{
			// TODO: alles
			Window = window;
		}
	}
}
