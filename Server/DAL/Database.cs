using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using DataTransfer.Types;

namespace Server.DAL
{
	public class Database
	{
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
		/// Get new connection
		/// </summary>
		public static Database Factory
		{
			get
			{
				Database result = new Database();
				result.Connect();
				return result;
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
				_connection.Close();
				_connection = null;
			}
		}

		public List<Contact> SearchContacts(string filter)
		{
			// variables
			DataTable contacts = SelectContacts(filter);

			// check result
			if (contacts == null) {
				return new List<Contact>();
			}

			return CreateContactList(contacts);
		}

		public List<Invoice> SearchInvoices(string filter)
		{
			throw new NotImplementedException();
		}

		public void UpdateContact(List<Contact> contacts)
		{
		}

		private DataTable SelectContacts(string filter)
		{
			// variables
			NpgsqlCommand command = new NpgsqlCommand("" +
				"SELECT * " +
				"FROM Contact " +
				"WHERE lower(UID) LIKE lower(:uid) " +
				"OR lower(Name) LIKE lower(:name) " +
				"OR lower(Forename) LIKE lower(:forename) " +
				"OR lower(Surname) LIKE lower(:surname) " +
				"OR lower(City) LIKE lower(:city)", _connection);

			// add parameters and prepare query
			command.Parameters.Add("uid", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("forename", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("surname", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("city", NpgsqlTypes.NpgsqlDbType.Text);
			command.Prepare();

			// add values
			filter = "%" + filter + "%";
			command.Parameters["uid"].Value = filter;
			command.Parameters["name"].Value = filter;
			command.Parameters["forename"].Value = filter;
			command.Parameters["surname"].Value = filter;
			command.Parameters["city"].Value = filter;

			return Select(command);
		}

		private List<Contact> CreateContactList(DataTable contacts)
		{
			// variables
			List<Contact> result = new List<Contact>();

			foreach (DataRow row in contacts.Rows) {
				int id = (int)row["ID"];
				string uid = (row["UID"].GetType().Name == "DBNull" ? null : (string)row["UID"]);
				string name = (row["Name"].GetType().Name == "DBNull" ? null : (string)row["Name"]);
				string forename = (row["Forename"].GetType().Name == "DBNull" ? null : (string)row["Forename"]);
				string surname = (row["Surname"].GetType().Name == "DBNull" ? null : (string)row["Surname"]);
				string title = (row["Title"].GetType().Name == "DBNull" ? null : (string)row["Title"]);
				string suffix = (row["Suffix"].GetType().Name == "DBNull" ? null : (string)row["Suffix"]);
				DateTime? birth = (row["BirthDate"].GetType().Name == "DBNull" ? new Nullable<DateTime>() : (DateTime)row["BirthDate"]);
				string street = (row["Street"].GetType().Name == "DBNull" ? null : row["Street"] as string);
				string number = (row["StreetNumber"].GetType().Name == "DBNull" ? null : row["StreetNumber"] as string);
				string code = (row["PostalCode"].GetType().Name == "DBNull" ? null : row["PostalCode"] as string);
				string city = (row["City"].GetType().Name == "DBNull" ? null : row["City"] as string);

				result.Add(new Contact(id, uid, name, title, forename, surname, suffix, birth, street, number, code, city));
			}

			return result;
		}

		/// <summary>
		/// Returns DataTable for given Sql Select Query.
		/// </summary>
		/// <param name="sql">Select query to execute</param>
		/// <returns>Selected rows or null if any error happened</returns>
		private DataTable Select(NpgsqlCommand command)
		{
			try {
				NpgsqlDataReader dbReader = command.ExecuteReader();
				DataTable dt = new DataTable();
				dt.Load(dbReader);
				return dt;
			} catch {
				// TODO: specific exception handling
				return null;
			}
		}

		/// <summary>
		/// Returns the number of affected rows for the given Insert, Update or Delete statement.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns>Returns number of effected rows.</returns>
		private int InsertUpdateDelete(NpgsqlCommand command)
		{
			try {
				return command.ExecuteNonQuery();
			} catch {
				// TODO: specific exception handling
				return -1;
			}
		}
	}
}
