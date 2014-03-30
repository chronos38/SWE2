using Server.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Converter
{
	public class ContactListConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(DataTable) {
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(DataTable)) {
				return true;
			}

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			List<Contact> result = new List<Contact>();
			DataTable table = value as DataTable;

			if (table == null) {
				return base.ConvertFrom(context, culture, value);
			}

			foreach (DataRow row in table.Rows) {
				result.Add(new Contact(row));
			}

			return result;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			DataTable result = new DataTable();
			List<Contact> contacts = value as List<Contact>;

			if (contacts == null) {
				return base.ConvertTo(context, culture, value, destinationType);
			}

			foreach (Contact contact in contacts) {
				result.ImportRow(contact.ToDataRow());
			}

			return result;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
