using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Server.DAL
{
	public class Database
	{
		private static volatile Database _instance = null;
		private static object _syncRoot = new object();
		private NpgsqlConnection _connection = null;

		private Database()
		{
		}

		~Database()
		{
			if (_connection != null) {
				_connection.Close();
			}
		}

		/// <summary>
		/// Get current instance
		/// </summary>
		public static Database Instance
		{
			get
			{
				if (_instance == null) {
					lock (_syncRoot) {
						if (_instance == null) {
							_instance = new Database();
						}
					}
				}

				return _instance;
			}
		}

		/// <summary>
		/// Connect to PostgreSQL database
		/// </summary>
		/// <param name="ip">IP-Address to connect</param>
		/// <param name="port">Which port to use</param>
		/// <param name="user">As which user to login</param>
		/// <param name="password">User password</param>
		/// <param name="db">Database to conenct to</param>
		public void Connect(string ip = "127.0.0.1", int port = 5432, string user = "sweadmin", string password = "swe", string db = "swedb")
		{
			// close old connection if there is any
			Close();

			lock (_syncRoot) {
				// connect database
				_connection = new NpgsqlConnection(
					String.Format(
						"Server={0};Port={1};User Id={2};Password={3};Database={4};",
						ip,
						port.ToString(),
						user,
						password,
						db
					)
				);

				_connection.Open();
			}
		}

		public bool IsConnected()
		{
			return (_connection != null && _connection.FullState == ConnectionState.Open);
		}

		/// <summary>
		/// Close current connection if there is any.
		/// </summary>
		public void Close()
		{
			if (_connection != null) {
				lock (_syncRoot) {
					_connection.Close();
					_connection = null;
				}
			}
		}

		/// <summary>
		/// Returns DataTable for given Sql Select Query.
		/// </summary>
		/// <param name="sql">Select query to execute</param>
		/// <returns>Selected rows or null if any error happened</returns>
		public DataTable Select(string sql)
		{
			if (_connection != null) {
				lock (_syncRoot) {
					try {
						NpgsqlCommand dbCommand = new NpgsqlCommand(sql, _connection);
						NpgsqlDataReader dbReader = dbCommand.ExecuteReader();
						DataTable dt = new DataTable();
						dt.Load(dbReader);
						return dt;
					} catch {
						// TODO: specific exception handling
						return null;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Returns the number of affected rows for the given Insert, Update or Delete statement.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns>Returns number of effected rows.</returns>
		public int InsertUpdateDelete(string sql)
		{
			if (_connection != null) {
				lock (_syncRoot) {
					try {
						NpgsqlCommand dbCommand = new NpgsqlCommand(sql, _connection);
						return dbCommand.ExecuteNonQuery();
					} catch {
						// TODO: specific exception handling
						return 0;
					}
				}
			}

			return 0;
		}
	}
}
