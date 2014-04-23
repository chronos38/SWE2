using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Types
{
	public class Address
	{
		public string Street { get; protected set; }
		public int StreetNumber { get; protected set; }
		public int PostalCode { get; protected set; }
		public string City { get; protected set; }

		public Address(string street, int streetNumber, int postalCode, string city)
		{
			Street = street;
			StreetNumber = streetNumber;
			PostalCode = postalCode;
			City = city;
		}
	}
}
