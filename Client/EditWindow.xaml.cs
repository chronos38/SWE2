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
		private Contact Contact { get; set; }

		public EditWindow()
		{
			InitializeComponent();
		}

		public EditWindow(object[] items)
		{
			this.Contact = new Contact(items);

			// initialize all
			InitializeComponent();
		}
	}
}
