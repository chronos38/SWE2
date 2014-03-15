using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Server
{
	class PgSqlDAL
	{
		string _connString = "Server=127.0.0.1;Port=5432;User Id=sweadmin;Password=swe;Database=swedb;";

		public PgSqlDAL(string connString)
		{
			_connString = connString;
		}
		public PgSqlDAL(string ip, int port, string user, string password, string db)
		{
			_connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", ip, port.ToString(), user, password, db);
		}
		/// <summary>
		/// Returns DataTable for given Sql Query.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public DataTable GetData(string sql)
		{
			NpgsqlConnection dbConn = new NpgsqlConnection(_connString);
			dbConn.Open();
			try
			{
				NpgsqlCommand dbCommand = new NpgsqlCommand(sql, dbConn);
				NpgsqlDataReader dbReader = dbCommand.ExecuteReader();
				DataTable dt = new DataTable();
				dt.Load(dbReader);
				dbConn.Close();

				return dt;
			}
			catch(Exception ex)
			{
				dbConn.Close();
				throw ex;
			}
		}

		/// <summary>
		/// Returns the number of affected rows for the given Insert, Update or Delete statement.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public int ModifyData(string sql)
		{
			NpgsqlConnection dbConn = new NpgsqlConnection(_connString);
			dbConn.Open();
			NpgsqlCommand dbCommand = new NpgsqlCommand(sql);
			int rows = dbCommand.ExecuteNonQuery();
			dbConn.Close();
			return rows;
		}

	}
}
