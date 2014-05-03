﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
	internal class ViewModel : DependencyObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string prop)
		{
			var temp = PropertyChanged;

			if (temp != null) {
				temp(this, new PropertyChangedEventArgs(prop));
			}
		}
	}
}
