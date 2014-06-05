using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Command
{
	internal class ContactSaveCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public async override void Execute(object parameter)
		{
			Proxy proxy = new Proxy();
			EditViewModel model = Model as EditViewModel;

			Contact contact = new Contact(
				model.ID,
				model.UID,
				model.Name,
				model.Title,
				model.Forename,
				model.Surname,
				model.Suffix,
				model.Birthday,
				model.CompanyID,
				model.Street,
				model.StreetNumber,
				model.ZIP,
				model.City
			);

			RPResult result = await proxy.SendContactAsync(contact);

			if (result.success == 0) {
				MessageBox.Show("Couldn't save contact, RPC-Call 'SendContactAsync' failed.", "Error", MessageBoxButton.OK);
			}

			Window.Close();
		}

		public ContactSaveCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
