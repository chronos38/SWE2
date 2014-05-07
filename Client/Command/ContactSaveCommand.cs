using Client.RPC;
using Client.ViewModel;
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

		public override void Execute(object parameter)
		{
			// TODO: implement save logic
			MessageBox.Show("Should save entry", "Save", MessageBoxButton.OK);

			/*Proxy proxy = new Proxy();
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
				model.Street,
				model.StreetNumber,
				model.ZIP,
				model.City
			);

			proxy.SendContactAsync(contact);*/
		}

		public ContactSaveCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
