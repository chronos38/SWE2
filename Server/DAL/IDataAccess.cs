using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	interface IDataAccess
	{
		DataRowCollection Rows { get; }
		DataColumn[] PrimaryKeys { get; }
		object Compute(string expression, string filter);
		DataRow[] Select();
		DataRow[] Select(string filter);
		DataRow[] Select(string filter, string sort);
	}
}
