using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Client.Data
{
	internal class Company
	{
		public int ID { get; private set; }

		public string Name { get; private set; }

		public Company(DataRow row)
		{
			ID = (int)row["ID"];
			Name = row["Name"] as string;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
