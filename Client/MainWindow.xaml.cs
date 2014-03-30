using Client.RPC;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Proxy Proxy { get; set; }
		private Key Pressed { get; set; }
		
		public MainWindow()
		{
			InitializeComponent();
			Proxy = new Proxy();

			// Base
			this.txtSearch.Text = "enter searchterm";
			//this.dgrdSearchResult.ItemsSource = this.BindEmployee().DefaultView;
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			Pressed = e.Key;
		}

		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && Pressed == Key.Return) {
				// TODO: search for content
				Task<RPResult> task = Proxy.SearchContactsAsync(this.txtSearch.Text);
				RPResult result = task.Result;
				this.dgrdSearchResult.ItemsSource = result.dt.DefaultView;
			}
		}

		private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtSearch.Text = "";
		}

		private void LoadSearchResultAsync(Task<RPResult> task)
		{
		}

		/*
		private DataTable BindEmployee()
		{
			try {
				DataTable employee = new DataTable();
				employee.Columns.Add(new DataColumn("Id", Type.GetType("System.Int32")));
				employee.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));

				int iterator = 0;

				while (++iterator < 101) {
					DataRow row = null;
					row = employee.NewRow();
					row["Id"] = iterator;
					row["Name"] = "Employee " + iterator;
					employee.Rows.Add(row);
				}
				return employee;
			} catch (Exception) {
				throw;
			}
		}
		*/
	}
}
