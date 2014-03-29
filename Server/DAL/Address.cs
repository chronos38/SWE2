using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class Address
	{
		public long ID { get; private set; }
		public string Street { get; private set; }
		public int StreetNumber { get; private set; }
		public int PostalCode { get; private set; }
		public string City { get; private set; }

		public Address(long id, string street, int streetNumber, int postalCode, string city)
		{
			ID = id;
			Street = street;
			StreetNumber = streetNumber;
			PostalCode = postalCode;
			City = city;
		}
	}
}
