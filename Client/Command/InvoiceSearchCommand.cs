using Client.Data;
using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Data;
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
			model.SearchResult = ConvertSearchView(result.dt);
		}

		public InvoiceSearchCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}

		private DataView ConvertSearchView(DataTable search)
		{
			throw new NotImplementedException();
		}
	}
}
