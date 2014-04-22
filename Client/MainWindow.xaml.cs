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

		private async void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			if (this.txtSearch.Text != "") {
				Task<RPResult> task = Proxy.SearchContactsAsync(this.txtSearch.Text);
				this.dgrdSearchResult.ItemsSource = await CreateSearchResult(task);
			}
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			Pressed = e.Key;
		}

		private async void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && Pressed == Key.Return && this.txtSearch.Text != "") {
				RPResult result = await Proxy.SearchContactsAsync(this.txtSearch.Text);
				this.dgrdSearchResult.ItemsSource = result.dt.DefaultView;
			}
		}

		private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtSearch.Text = "";
		}

		private async Task<DataView> CreateSearchResult(Task<RPResult> rpresult)
		{
			// variables
			DataTable table = (await rpresult).dt;
			DataTable result = new DataTable("Contact");

			// add columns
			result.Columns.Add(new DataColumn("ID", typeof(int)));
			result.Columns.Add(new DataColumn("Name", typeof(string)));
			result.Columns.Add(new DataColumn("Forename", typeof(string)));
			result.Columns.Add(new DataColumn("Surname", typeof(string)));

			// copy values to view
			foreach (DataRow row in table.Rows) {
				// create row and insert values
				DataRow insert = result.NewRow();
				insert["ID"] = row["ID"];
				insert["Name"] = row["Name"];
				insert["Forename"] = row["Forename"];
				insert["Surname"] = row["Surname"];

				// add row
				result.Rows.Add(insert);
			}

			// return
			return result.DefaultView;
		}

		/*
		private DataTable BindEmployee()
		{
			try {
				DataTable employee = new DataTable();
				employee.Columns.Add(new DataColumn("ID", Type.GetType("System.Int32")));
				employee.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));

				int iterator = 0;

				while (++iterator < 101) {
					DataRow row = null;
					row = employee.NewRow();
					row["ID"] = iterator;
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
