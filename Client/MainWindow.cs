using Client.RPC;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client
{
	public partial class MainWindow : Window
	{
		private Window Editor { get; set; }
		private Proxy Proxy { get; set; }
		private Key Pressed { get; set; }
		
		private void OpenEditor(DataRowView view)
		{
			// check row
			if (view == null) {
				return;
			}

			// variables
			object[] items = view.Row.ItemArray;

			// open edit window
			if (Editor == null) {
				Editor = new EditWindow(new Contact(items));
				Editor.ShowDialog();
				Editor = null;
			}
		}
	}
}
