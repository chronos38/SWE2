using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command
{
	class InvoicePrintCommand : Command
	{
		public override void Execute(object parameter)
		{
			// TODO: alles
		}

		public InvoicePrintCommand(EditInvoiceWindow window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
