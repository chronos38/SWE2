using Client.ViewModel;
using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Client.Converter
{
	internal class ContactCompanyConverter : IValueConverter
	{
		private static readonly ContactCompanyConverter converter = new ContactCompanyConverter();

		private ContactCompanyConverter()
		{
		}

		public static ContactCompanyConverter Instance
		{
			get
			{
				return converter;
			}
		}

		// Summary:
		//     Converts a value.
		//
		// Parameters:
		//   value:
		//     The value produced by the binding source.
		//
		//   targetType:
		//     The type of the binding target property.
		//
		//   parameter:
		//     The converter parameter to use.
		//
		//   culture:
		//     The culture to use in the converter.
		//
		// Returns:
		//     A converted value. If the method returns null, the valid null value is used.
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			List<Contact> contacts = value as List<Contact>;
			List<CompanyViewModel> result = new List<CompanyViewModel>();

			if (contacts == null) {
				return result;
			}

			foreach (Contact contact in contacts) {
				result.Add(new CompanyViewModel(contact.ID, contact.UID, contact.Name));
			}

			return result;
		}
		//
		// Summary:
		//     Converts a value.
		//
		// Parameters:
		//   value:
		//     The value that is produced by the binding target.
		//
		//   targetType:
		//     The type to convert to.
		//
		//   parameter:
		//     The converter parameter to use.
		//
		//   culture:
		//     The culture to use in the converter.
		//
		// Returns:
		//     A converted value. If the method returns null, the valid null value is used.
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ItemCollection items = value as ItemCollection;
			List<Contact> result = new List<Contact>();

			if (items == null) {
				return result;
			}

			foreach (object item in items) {
				CompanyViewModel model = item as CompanyViewModel;

				result.Add(new Contact(
						model.ID,
						model.UID,
						model.Name,
						null,
						null,
						null,
						null,
						null,
						null,
						null,
						null,
						null,
						null
					)
				);
			}

			return result;
		}
	}
}
