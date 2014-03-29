using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class AdditionalAddress : Address
	{
		public string Type { get; private set; }

		public AdditionalAddress(string street, int streetNumber, int postalCode, string city, string type) :
			base(street, streetNumber, postalCode, city)
		{
			Type = type;
		}
	}
}
