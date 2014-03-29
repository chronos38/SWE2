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

		private DataTable SelectContacts(string filter)
		{
			string query = String.Format("" +
				"SELECT Contact.ID, Contact.Name, PersonData.Title, PersonData.Forename, PersonData.Surname, PersonData.Suffix, PersonData.BirthDate " +
				"FROM Contact " +
				"JOIN PersonData " +
				"ON Contact.fk_PersonData = PersonData.ID " +
				"WHERE Contact.Name = '{0}'" +
				"OR PersonData.Forename = '{0}' " +
				"OR PersonData.Surname = '{0}';", filter);
			return Select(query);
		}

		private List<Contact> CreateContactList(DataTable contacts)
		{
			// variables
			List<Contact> result = new List<Contact>();
			int id = -1;
			string name = null;
			string forename = null;
			string surname = null;
			string title = null;
			string suffix = null;
			DateTime birth = new DateTime();

			foreach (DataRow row in contacts.Rows) {
				id = (int)row["ID"];
				name = (row["Name"].GetType().Name == "DBNull" ? null : (string)row["Name"]);
				forename = (row["Forename"].GetType().Name == "DBNull" ? null : (string)row["Forename"]);
				surname = (row["Surname"].GetType().Name == "DBNull" ? null : (string)row["Surname"]);
				title = (row["Title"].GetType().Name == "DBNull" ? null : (string)row["Title"]);
				suffix = (row["Suffix"].GetType().Name == "DBNull" ? null : (string)row["Suffix"]);
				birth = (row["BirthDate"].GetType().Name == "DBNull" ? new DateTime() : (DateTime)row["BirthDate"]);

				Company company = new Company(id, name);
				Person person = new Person(forename, surname, title, suffix, birth);
				result.Add(new Contact(company, person, null, null));
			}

			return result;
		}

		/// <summary>
		/// Returns DataTable for given Sql Select Query.
		/// </summary>
		/// <param name="sql">Select query to execute</param>
		/// <returns>Selected rows or null if any error happened</returns>
		private DataTable Select(string sql)
		{
			if (_connection != null) {
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

			return null;
		}

		/// <summary>
		/// Returns the number of affected rows for the given Insert, Update or Delete statement.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns>Returns number of effected rows.</returns>
		private int InsertUpdateDelete(string sql)
		{
			if (_connection != null) {
				try {
					NpgsqlCommand dbCommand = new NpgsqlCommand(sql, _connection);
					return dbCommand.ExecuteNonQuery();
				} catch {
					// TODO: specific exception handling
					return 0;
				}
			}

			return 0;
		}
	}
}
