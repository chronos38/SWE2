using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class Company
	{
		public long ID { get; private set; }
		public string Name { get; private set; }

		public Company(long id, string name)
		{
			ID = id;
			Name = name;
		}
	}
}
