﻿using System;
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
				"SELECT Contact.ID, Contact.UID, Contact.Name, Contact.Title, Contact.Forename, Contact.Surname, Contact.Suffix, Contact.BirthDate " +
				"FROM Contact " +
				"JOIN Address " +
				"ON Contact.fk_Address = Address.ID " +
				"WHERE lower(Contact.UID) = lower(:uid) " +
				"OR lower(Contact.Name) = lower(:name) " +
				"OR lower(Contact.Forename) = lower(:forename) " +
				"OR lower(Contact.Surname) = lower(:surname) " +
				"OR lower(Address.Street) = lower(:street) " +
				"OR lower(Address.City) = lower(:city)", _connection);

			// add parameters and prepare query
			command.Parameters.Add("uid", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("forename", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("surname", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("street", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("city", NpgsqlTypes.NpgsqlDbType.Text);
			command.Prepare();

			// add values
			command.Parameters["uid"].Value = filter;
			command.Parameters["name"].Value = filter;
			command.Parameters["forename"].Value = filter;
			command.Parameters["surname"].Value = filter;
			command.Parameters["street"].Value = filter;
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
				DateTime birth = (row["BirthDate"].GetType().Name == "DBNull" ? new DateTime() : (DateTime)row["BirthDate"]);

				result.Add(new Contact(uid, name, title, forename, surname, suffix, birth, GetAddress(id), GetAdditionalAddresses(id)));
			}

			return result;
		}

		private Address GetAddress(int id)
		{
			// TODO: SQL-Query for main address
			return null;
		}

		private List<AdditionalAddress> GetAdditionalAddresses(int id)
		{
			// TODO: SQL-Query for addresses
			return null;
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
