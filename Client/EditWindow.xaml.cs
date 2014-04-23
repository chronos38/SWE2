using DataTransfer.Converter;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Client
{
	/// <summary>
	/// Interaction logic for EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		public EditWindow(Contact contact)
		{
			this.Contact = contact;

			// initialize all
			InitializeComponent();

			// set content
			IntializeContact();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Should save entry", "Save", MessageBoxButton.OK);
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
				MessageBox.Show("Should delete entry", "Delete", MessageBoxButton.OK);
			}
		}
	}
}
