using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command
{
	class InvoiceSaveCommand : Command
	{
		public override void Execute(object parameter)
		{
			// TODO: alles
		}

		public InvoiceSaveCommand(EditInvoiceWindow window, EditInvoiceViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
