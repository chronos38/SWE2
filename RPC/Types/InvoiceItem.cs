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
		public int Quantity { get; private set; }
		public float UnitPrice { get; private set; }
		public float VAT { get; private set; }
		public float Gross { get; private set; }
		public float Net { get; private set; }

		public InvoiceItem(string name, int quantity, float unitPrice, float vat)
		{
			Name = name;
			quantity = Quantity;
			UnitPrice = unitPrice;
			VAT = vat;
			Gross = quantity * unitPrice * vat;
			Net = quantity * unitPrice;
		}
	}
}
