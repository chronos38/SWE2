using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			string sql = "SELECT pk_test, test_data FROM test";
			PgSqlDAL dal = new PgSqlDAL("127.0.0.1", 5432, "sweadmin", "swe", "swedb");
			DataTable result = dal.GetData(sql);
			foreach(DataRow row in result.Rows)
			{
				Console.WriteLine(row[0] + " | " + row[1]);
			}
			Console.ReadKey();

		}
	}
}