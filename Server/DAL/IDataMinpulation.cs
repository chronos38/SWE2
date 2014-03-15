using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	interface IDataManipulation
	{
		void AcceptChanges();
		void Add(DataRow row);
		void Add(DataRow[] rows);
		void ImportRow(DataRow row);
		DataRow NewRow();
	}
}
