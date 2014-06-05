using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command
{
	class InvoiceCancelCommand : Command
	{
		public override void Execute(object parameter)
		{
			// TODO: alles
		}

		public InvoiceCancelCommand(EditInvoiceWindow window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
