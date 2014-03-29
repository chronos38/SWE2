using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class Contact
	{
		public Company Company { get; private set; }
		public Person Person { get; private set; }
		public Address Address { get; private set; }
		public List<Address> AdditionalAddresses { get; private set; }

		public Contact(Company company, Person person, Address address, List<Address> addresses)
		{
			Company = company;
			Person = person;
			Address = address;
			AdditionalAddresses = addresses;
		}
	}
}
