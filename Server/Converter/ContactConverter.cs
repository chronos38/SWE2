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
	public class ContactConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(DataRow)) {
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(DataRow)) {
				return true;
			}

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			// variables
			DataRow row = value as DataRow;

			// conversion
			if (row == null) {
				return base.ConvertFrom(context, culture, value);
			}

			return new Contact(row);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			// variables
			Contact contact = value as Contact;

			// conversion
			if (contact == null) {
				return base.ConvertTo(context, culture, value, destinationType);
			}

			return contact.ToDataRow();
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
