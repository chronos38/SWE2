using Client.Command;
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
			Search = new MainSearchCommand(window, this);
			Open = new MainOpenCommand(window, this);
			New = new MainNewCommand(window, this);
			InvoiceSearch = new InvoiceSearchCommand(window, this);
			InvoiceOpen = new InvoiceOpenCommand(window, this);

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

			Window.dgrdInvoiceSearchResult.MouseDoubleClick += (object sender, MouseButtonEventArgs e) => {
				// variables
				DependencyObject source = (DependencyObject)e.OriginalSource;
				DataGridRow rows = UIHelper.TryFindParent<DataGridRow>(source);

				// check rows
				if (rows == null) {
					return;
				}

				// try casting to row
				InvoiceOpen.Execute(rows.Item);

				e.Handled = true;
			};
		}

		#region Contact
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

		public ICommand Search { get; private set; }
		public ICommand Open { get; private set; }
		public ICommand New { get; private set; }
		#endregion

		#region Invoice
		private string _invoiceSearchText = null;
		public string InvoiceSearchText
		{
			get { return _invoiceSearchText; }
			set
			{
				if (_invoiceSearchText != value) {
					_invoiceSearchText = value;
					OnPropertyChanged("InvoiceSearchText");
				}
			}
		}

		private DateTime? _from = null;
		public DateTime? DateFrom
		{
			get { return _from; }
			set
			{
				if (_from != value) {
					_from = value;
					OnPropertyChanged("DateFrom");
				}
			}
		}

		private DateTime? _to = null;
		public DateTime? DateTo
		{
			get { return _to; }
			set
			{
				if (_to != value) {
					_to = value;
					OnPropertyChanged("DateTo");
				}
			}
		}

		private DataView _invoiceView = null;
		public DataView InvoiceSearchResult
		{
			get { return _invoiceView; }
			set
			{
				if (_invoiceView != value) {
					_invoiceView = value;
					OnPropertyChanged("InvoiceSearchResult");
				}
			}
		}

		public ICommand InvoiceSearch { get; private set; }
		public ICommand InvoiceOpen { get; private set; }
		#endregion
	}
}
