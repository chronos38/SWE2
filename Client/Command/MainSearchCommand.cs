using Client.RPC;
using Client.ViewModel;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Command
{
	internal class MainSearchCommand : Command
	{
		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public async override void Execute(object parameter)
		{
			string text = parameter as string;

			if (!string.IsNullOrWhiteSpace(text)) {
				SearchViewModel model = Model as SearchViewModel;
				Proxy proxy = new Proxy();
				RPResult result = await proxy.SearchContactsAsync(text);
				model.SearchResult = result.dt.DefaultView;

				if (result.dt.Rows.Count == 1) {
					model.Open.Execute(model.SearchResult[0]);
				}
			}
		}

		public MainSearchCommand(Window window, SearchViewModel model)
		{
			Window = window;
			Model = model;
		}
	}
}
