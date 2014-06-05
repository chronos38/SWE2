using Client.RPC;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Command
{
	internal class PersonDeleteCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			Proxy proxy = new Proxy();
			EditViewModel model = Model as EditViewModel;
			EditWindow window = Window as EditWindow;

			if (window == null || model == null) {
				return;
			}

			proxy.DeleteCompany(model.ID);
			model.Company = null;
			model.CompanyID = null;
			model.Checked = false;
		}

		public PersonDeleteCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
