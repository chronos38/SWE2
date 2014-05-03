using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModel
{
	internal class SearchViewModel : ViewModel
	{
		private MainWindow Window { get; set; }

		public SearchViewModel(Window window)
		{
			Window = window as MainWindow;
			Search = new Command.MainSearchCommand(window, this);
			Open = new Command.MainOpenCommand(window, this);
			New = new Command.MainNewCommand(window, this);

			Window.dgrdSearchResult.MouseDoubleClick += (object sender, MouseButtonEventArgs e) => {
				// variables
				DependencyObject source = (DependencyObject)e.OriginalSource;
				DataGridRow rows = UIHelper.TryFindParent<DataGridRow>(source);

				// check rows
				if (rows == null) {
					return;
				}

				// try casting to row
				Open.Execute(rows.Item);

				e.Handled = true;
			};
		}

		private string _searchText = null;
		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (_searchText != value) {
					_searchText = value;
					OnPropertyChanged("SearchText");
				}
			}
		}

		private DataView _dataView = null;
		public DataView SearchResult
		{
			get { return _dataView; }
			set
			{
				if (_dataView != value) {
					_dataView = value;
					OnPropertyChanged("SearchResult");
				}
			}
		}

		public ICommand Search { get; set; }
		public ICommand Open { get; set; }
		public ICommand New { get; set; }
	}
}
