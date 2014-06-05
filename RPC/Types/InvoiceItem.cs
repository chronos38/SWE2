using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Types
{
	[Serializable]
	public class InvoiceItem
	{
		public string Name { get; private set; }
		public int? Quantity { get; private set; }
		public double? UnitPrice { get; private set; }
		public double? VAT { get; private set; }
		public double? Gross { get; private set; }
		public double? Net { get; private set; }

		public InvoiceItem()
		{
			Name = null;
			Quantity = null;
			UnitPrice = null;
			VAT = null;
			Gross = null;
			Net = null;
		}

		public InvoiceItem(string name, int? quantity, double? unitPrice, double? vat)
		{
			Name = name;
			Quantity = quantity;
			UnitPrice = unitPrice;
			VAT = vat;

			if (quantity != null && unitPrice != null) {
				Net = quantity * unitPrice;

				if (vat != null) {
					Gross = Net * (1 + (VAT / 100.0));
				} else {
					Gross = null;
				}
			} else {
				Gross = null;
				Net = null;
			}
		}
	}
}
