using Client.Converter;
using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using DataTransfer.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Command
{
	internal class PersonSearchCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public async override void Execute(object parameter)
		{
			ContactListConverter con = new ContactListConverter();
			Proxy proxy = new Proxy();
			EditWindow window = Window as EditWindow;
			EditViewModel editModel = Model as EditViewModel;

			if (window == null || editModel == null) {
				return;
			} else {
				editModel.Delete.Execute(null);
			}

			RPResult result = await proxy.GetCompaniesAsync();
			List<CompanyViewModel> models = ContactCompanyConverter.Instance.Convert(con.ConvertFrom(result.dt), typeof(ItemCollection), null, null) as List<CompanyViewModel>;

			foreach (CompanyViewModel model in models) {
				int index = window.cmbPersonCompany.Items.Add(model);

				if (parameter != null) {
					int? id = parameter as int?;

					if (id != null && id == model.ID) {
						window.cmbPersonCompany.SelectedIndex = index;
					}
				}
			}
		}

		public PersonSearchCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
