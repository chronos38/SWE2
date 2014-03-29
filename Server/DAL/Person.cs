using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	public class Person
	{
		public long ID { get; private set; }
		public string Forename { get; private set; }
		public string Surname { get; private set; }
		public string Title { get; private set; }
		public string Suffix { get; private set; }
		public DateTime Birthday { get; private set; }

		public Person(long id, string forename, string surname, string title, string suffix, DateTime birthday)
		{
			ID = id;
			Forename = forename;
			Surname = surname;
			Title = title;
			Suffix = suffix;
			Birthday = birthday;
		}
	}
}
