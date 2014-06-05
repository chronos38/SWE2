using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using DataTransfer.Converter;
using System;
using System.Collections.Generic;
using System.Data;
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
			int? id = parameter as int?;
			string company = parameter as string;
			ContactListConverter con = new ContactListConverter();
			Proxy proxy = new Proxy();
			EditWindow window = Window as EditWindow;
			EditViewModel model = Model as EditViewModel;

			if (!string.IsNullOrWhiteSpace(company)) {
				RPResult result = await proxy.SearchCompany(model.ID, company);

				if (result.dt != null && result.dt.Rows.Count == 1) {
					DataRow row = result.dt.Rows[0];
					model.Company = row["Name"] as string;
					model.CompanyID = (int)row["ID"];
					model.Checked = true;
				} else {
					window.cmbPersonCompany.Items.Clear();
					model.Checked = false;

					foreach (DataRow row in result.dt.Rows) {
						window.cmbPersonCompany.Items.Add(row["Name"]);
					}
				}
			} else if (id != null) {
				RPResult result = await proxy.SetCompany(id);
				DataRow row = result.dt.Rows[0];
				model.Company = row["Name"] as string;
				model.CompanyID = (int)row["ID"];
				model.Checked = true;
			}
		}

		public PersonSearchCommand(Window window, EditViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
