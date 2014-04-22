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
				RPResult result = await Proxy.SearchContactsAsync(this.txtSearch.Text);
				this.dgrdSearchResult.ItemsSource = result.dt.DefaultView;
			}

			e.Handled = true;
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

			e.Handled = true;
		}

		private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtSearch.Text = "";
		}

		private void dgrdSearchResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// variables
			DependencyObject source = (DependencyObject)e.OriginalSource;
			var row = UIHelper.GetParentObject(source);

			e.Handled = true;
		}
	}
}
