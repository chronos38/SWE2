﻿using Client.ViewModel;
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
	/// Interaction logic for EditInvoiceWindow.xaml
	/// </summary>
	public partial class EditInvoiceWindow : Window
	{
		public EditInvoiceWindow()
		{
			InitializeComponent();
		}

		public EditInvoiceWindow(Invoice invoice)
		{
			InitializeComponent();
			this.DataContext = new EditInvoiceViewModel(this, invoice);
		}

		public EditInvoiceWindow(int contact)
		{
			InitializeComponent();
			this.DataContext = new EditInvoiceViewModel(this, contact);
		}
	}
}
