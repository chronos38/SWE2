using Client.Data;
using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Command
{
	internal class InvoiceSearchCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public async override void Execute(object parameter)
		{
			SearchViewModel model = Model as SearchViewModel;
			Proxy proxy = new Proxy();
			RPResult result = await proxy.SearchInvoicesAsync(new InvoiceSearchData(
				model.DateFrom,
				model.DateTo,
				model.InvoiceSearchText
			));
			model.InvoiceSearchResult = ConvertSearchView(result.dt);

			if (result.dt.Rows.Count == 1) {
				model.InvoiceOpen.Execute(model.InvoiceSearchResult[0]);
			}
		}

		public InvoiceSearchCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}

		private DataView ConvertSearchView(DataTable search)
		{
			search.Columns.Add("Amount");
			//search.Columns["Amount"].DataType = typeof(float);

			foreach (DataRow row in search.Rows) {
				Invoice invoice = new Invoice(row);
				double amount = 0;

				foreach (InvoiceItem item in invoice.Items) {
					if (item.Gross != null) {
						amount += item.Gross.Value;
					}
				}

				row["Amount"] = amount.ToString("C", CultureInfo.CreateSpecificCulture("de-AT"));
			}

			return search.DefaultView;
		}
	}
}
