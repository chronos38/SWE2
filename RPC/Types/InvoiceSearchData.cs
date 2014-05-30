using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Data
{
	[Serializable]
	public class InvoiceSearchData
	{
		public DateTime? From { get; private set; }
		public DateTime? To { get; private set; }
		public string Contact { get; private set; }

		public InvoiceSearchData(DateTime? from, DateTime? to, string contact)
		{
			From = from;
			To = to;
			Contact = contact;
		}
	}
}
