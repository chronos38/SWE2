using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			NpgsqlConnection dbConn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=sweadmin;Password=swe;Database=swedb;");
			dbConn.Open();
			string sql = "SELECT pk_test, test_data FROM test";
			NpgsqlCommand dbCommand = new NpgsqlCommand(sql, dbConn);
			NpgsqlDataReader dbDataReader = dbCommand.ExecuteReader();
			while(dbDataReader.Read())
			{
				String id = dbDataReader[0].ToString();
				String data = dbDataReader[1].ToString(); 
				Console.WriteLine(id + " | " + data);
			}
			dbConn.Close();
			Console.ReadKey();

		}
	}
}