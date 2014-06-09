using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using DataTransfer.Types;
using Client.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Server.DAL
{
	// TODO: Aufteilen der Datenbankverbindung mit den Contact-Auslesen

	public class Database : IDatabase
	{
		private NpgsqlConnection _connection = null;

		public Database()
		{
		}

		~Database()
		{
			if (_connection != null) {
				_connection.Close();
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

		List<Invoice> SelectInvoice(int p)
		{
			// variables
			string query = "SELECT Contact.UID, Contact.Name, Contact.Forename, Contact.Surname, Invoice.fk_Contact, " +
				"Invoice.ID, Invoice.Date, Invoice.Maturity, Invoice.Comment, Invoice.Message, Invoice.Type " +
				"FROM Invoice JOIN Contact ON Invoice.fk_Contact = Contact.ID WHERE Invoice.ID = " + p.ToString();

			// execute
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			return CreateInvoiceList(Select(command));
		}
		
		public int DeleteInvoiceItem(int p)
		{
			string item = "delete from invoiceitem where id=:id";
			string position = "delete from invoiceposition where fk_invoiceitem=:id";

			NpgsqlCommand command = new NpgsqlCommand(position, _connection);
			command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();
			command.Parameters["id"].Value = p;
			InsertUpdateDelete(command);

			command = new NpgsqlCommand(item, _connection);
			command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();
			command.Parameters["id"].Value = p;
			InsertUpdateDelete(command);

			return 1;
		}

		public int UpsertInvoice(DataTable dataTable)
		{
			foreach (DataRow row in dataTable.Rows) {
				if ((int)row["ID"] == -1) {
					InsertInvoice(row);
				} else {
					UpdateInvoice(row);
				}
			}

			return 0;
		}

		private int InsertInvoice(DataRow row)
		{
			// Invoice items
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(row["Items"] as byte[]);
			List<InvoiceItem> items = (List<InvoiceItem>)binaryFormatter.Deserialize(memoryStream);

			// query
			string query = "insert into invoice(id,date,maturity,comment,message,type,readonly,fk_contact)values(DEFAULT,:date,:maturity,:comment,:message,:type,:readonly,:contact)returning id";
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			
			// parameters
			command.Parameters.Add("date", NpgsqlTypes.NpgsqlDbType.Date);
			command.Parameters.Add("maturity", NpgsqlTypes.NpgsqlDbType.Date);
			command.Parameters.Add("comment", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("message", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("type", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("readonly", NpgsqlTypes.NpgsqlDbType.Boolean);
			command.Parameters.Add("contact", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();

			if (row["Date"] == null) {
				command.Parameters["date"].Value = DBNull.Value;
			} else {
				command.Parameters["date"].Value = (DateTime)row["Date"];
			}

			if (row["Date"] == null) {
				command.Parameters["maturity"].Value = DBNull.Value;
			} else {
				command.Parameters["maturity"].Value = (DateTime)row["Maturity"];
			}

			command.Parameters["comment"].Value = row["Comment"] as string;
			command.Parameters["message"].Value = row["Message"] as string;
			command.Parameters["type"].Value = row["Type"] as string;
			command.Parameters["readonly"].Value = (bool)row["ReadOnly"];
			command.Parameters["contact"].Value = (int)row["Contact"];

			return UpsertInvoiceItems(items, Select(command));
		}

		private int UpsertInvoiceItems(List<InvoiceItem> items, DataTable dataTable)
		{
			if (dataTable == null || dataTable.Rows.Count > 1) {
				throw new ArgumentException();
			}

			DataRow row = dataTable.Rows[0];
			return UpsertInvoiceItems(items, (int)row["id"]);
		}

		private int UpsertInvoiceItems(List<InvoiceItem> items, int p)
		{
			NpgsqlCommand command = null;
			string insert = "insert into invoiceitem(id,name,unitprice,quantity,vat)values(DEFAULT,:name,:unitprice,:quantity,:vat)returning id";
			string update = "update invoiceitem set name=:name,unitprice=:unitprice,quantity=:quantity,vat=:vat where id=:id";

			foreach (InvoiceItem item in items) {
				if (item.ID < 0) {
					command = new NpgsqlCommand(insert, _connection);
				} else {
					command = new NpgsqlCommand(update, _connection);
					command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
				}

				// parameters
				command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Text);
				command.Parameters.Add("unitprice", NpgsqlTypes.NpgsqlDbType.Double);
				command.Parameters.Add("quantity", NpgsqlTypes.NpgsqlDbType.Integer);
				command.Parameters.Add("vat", NpgsqlTypes.NpgsqlDbType.Double);
				command.Prepare();
				command.Parameters["name"].Value = item.Name as string;

				if (item.ID > -1) {
					command.Parameters["id"].Value = item.ID;
				}

				if (item.UnitPrice == null) {
					command.Parameters["unitprice"].Value = DBNull.Value;
				} else {
					command.Parameters["unitprice"].Value = item.UnitPrice;
				}

				if (item.Quantity == null) {
					command.Parameters["quantity"].Value = DBNull.Value;
				} else {
					command.Parameters["quantity"].Value = item.Quantity;
				}

				if (item.VAT == null) {
					command.Parameters["vat"].Value = DBNull.Value;
				} else {
					command.Parameters["vat"].Value = item.VAT;
				}

				if (item.ID < 0) {
					InserInvoicePosition(p, Select(command));
				} else {
					InsertUpdateDelete(command);
				}
			}

			return 1;
		}

		private void InserInvoicePosition(int p, DataTable dataTable)
		{
			if (dataTable == null || dataTable.Rows.Count > 1) {
				throw new ArgumentException();
			}

			string query = "insert into invoiceposition(fk_invoice,fk_invoiceitem)values(:invoice,:invoiceitem)";
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			
			// parameters
			command.Parameters.Add("invoice", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Parameters.Add("invoiceitem", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();
			command.Parameters["invoice"].Value = p;
			command.Parameters["invoiceitem"].Value = (int)dataTable.Rows[0]["id"];
			InsertUpdateDelete(command);
		}

		private int UpdateInvoice(DataRow row)
		{
			// Invoice items
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(row["Items"] as byte[]);
			List<InvoiceItem> items = (List<InvoiceItem>)binaryFormatter.Deserialize(memoryStream);

			// query
			string query = "update invoice set date=:date,maturity=:maturity,comment=:comment,message=:message,type=:type,readonly=:readonly,fk_contact=:contact where id=:id";
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);

			// parameters
			command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Parameters.Add("date", NpgsqlTypes.NpgsqlDbType.Date);
			command.Parameters.Add("maturity", NpgsqlTypes.NpgsqlDbType.Date);
			command.Parameters.Add("comment", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("message", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("type", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("readonly", NpgsqlTypes.NpgsqlDbType.Boolean);
			command.Parameters.Add("contact", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();

			if (row["Date"] == null) {
				command.Parameters["date"].Value = DBNull.Value;
			} else {
				command.Parameters["date"].Value = (DateTime)row["Date"];
			}

			if (row["Date"] == null) {
				command.Parameters["maturity"].Value = DBNull.Value;
			} else {
				command.Parameters["maturity"].Value = (DateTime)row["Maturity"];
			}

			command.Parameters["id"].Value = (int)row["ID"];
			command.Parameters["comment"].Value = row["Comment"] as string;
			command.Parameters["message"].Value = row["Message"] as string;
			command.Parameters["type"].Value = row["Type"] as string;
			command.Parameters["readonly"].Value = (bool)row["ReadOnly"];
			command.Parameters["contact"].Value = (int)row["Contact"];

			return UpsertInvoiceItems(items, (int)row["ID"]);
		}

		public DataTable SearchCompany(int p1, string p2)
		{
			string query = "select ID, UID, Name from Contact where lower(Name) like lower(:name)";
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Text);
			command.Prepare();
			command.Parameters["name"].Value = "%" + p2 + "%";
			DataTable table = Select(command);

			if (table.Rows.Count == 1) {
				query = "update Contact set Company=" + (int)table.Rows[0]["ID"] + " where ID=:id";
				command = new NpgsqlCommand(query, _connection);
				command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
				command.Prepare();
				command.Parameters["id"].Value = p1;
				InsertUpdateDelete(command);
			}

			return table;
		}

		public int DeleteCompany(int p)
		{
			string query = "update Contact set Company=null where ID=:id";
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();
			command.Parameters["id"].Value = p;
			return InsertUpdateDelete(command);
		}

		public DataTable GetCompany(int p)
		{
			string query = "select ID, Name from Contact where ID=:id";
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Prepare();
			command.Parameters["id"].Value = p;
			return Select(command);
		}

		public List<Contact> GetCompanies()
		{
			DataTable companies = SelectCompanies();

			if (companies == null) {
				return new List<Contact>();
			}

			return CreateContactList(companies);
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

		public List<Invoice> SearchInvoices(int id, DateTime? from, DateTime? to)
		{
			// variables
			string query = "SELECT Contact.UID, Contact.Name, Contact.Forename, Contact.Surname, Invoice.fk_Contact, " +
				"Invoice.ID, Invoice.Date, Invoice.Maturity, Invoice.Comment, Invoice.Message, Invoice.Type, Invoice.ReadOnly " +
				"FROM Invoice JOIN Contact ON Invoice.fk_Contact = Contact.ID WHERE (Contact.ID = " + id.ToString() + ")";

			if (from != null) {
				query += "and(Invoice.Date >= :from)";
			}

			if (to != null) {
				query += "and(Invoice.Date <= :to)";
			}

			// execute
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);

			if (from != null) {
				command.Parameters.Add("from", NpgsqlTypes.NpgsqlDbType.Date);
			}

			if (to != null) {
				command.Parameters.Add("to", NpgsqlTypes.NpgsqlDbType.Date);
			}

			command.Prepare();

			if (from != null) {
				command.Parameters["from"].Value = from;
			}

			if (to != null) {
				command.Parameters["to"].Value = to;
			}

			return CreateInvoiceList(Select(command));
		}

		public List<Invoice> SearchInvoices(InvoiceSearchData data)
		{
			// variables
			DataTable invoices = SelectInvoices(data);

			// check result
			if (invoices == null) {
				return new List<Invoice>();
			}

			return CreateInvoiceList(invoices);
		}

		private DataTable SelectInvoices(InvoiceSearchData data)
		{
			// variables
			string from = data.From != null ? "Invoice.Date >= :from" : null;
			string to = data.To != null ? "Invoice.Date <= :to" : null;
			string contact = data.Contact != null ? "lower(Contact.UID) LIKE lower(:contact) OR lower(Contact.Name) LIKE lower(:contact) OR lower(Contact.Forename) LIKE lower(:contact) OR lower(Contact.Surname) LIKE lower(:contact)" : null;
			string query = "SELECT Contact.UID, Contact.Name, Contact.Forename, Contact.Surname, Invoice.fk_Contact, " +
				"Invoice.ID, Invoice.Date, Invoice.Maturity, Invoice.Comment, Invoice.Message, Invoice.Type " +
				"FROM Invoice JOIN Contact ON Invoice.fk_Contact = Contact.ID WHERE TRUE";

			// create where clause
			if (from != null) {
				query += " AND (" + from + ")";
			}

			if (to != null) {
				query += " AND (" + to + ")";
			}

			if (contact != null) {
				query += " AND (" + contact + ")";
			}

			// execute
			NpgsqlCommand command = new NpgsqlCommand(query, _connection);
			AddInvoiceParameter(command, data);
			return Select(command);
		}

		private void AddInvoiceParameter(NpgsqlCommand command, InvoiceSearchData data)
		{
			if (data.From != null) {
				command.Parameters.Add("from", NpgsqlTypes.NpgsqlDbType.Date);
			}

			if (data.To != null) {
				command.Parameters.Add("to", NpgsqlTypes.NpgsqlDbType.Date);
			}

			if (data.Contact != null) {
				command.Parameters.Add("contact", NpgsqlTypes.NpgsqlDbType.Text);
			}

			command.Prepare();

			if (data.From != null) {
				command.Parameters["from"].Value = data.From.Value;
			}

			if (data.To != null) {
				command.Parameters["to"].Value = data.To.Value;
			}

			if (data.Contact != null) {
				command.Parameters["contact"].Value = "%" + data.Contact + "%";
			}
		}

		private List<Invoice> CreateInvoiceList(DataTable invoices)
		{
			string query = "SELECT InvoiceItem.ID, InvoiceItem.Name, InvoiceItem.UnitPrice, InvoiceItem.Quantity, InvoiceItem.VAT " +
				"FROM InvoiceItem JOIN InvoicePosition ON InvoiceItem.ID = InvoicePosition.fk_InvoiceItem JOIN Invoice ON InvoicePosition.fk_Invoice = Invoice.ID WHERE Invoice.ID = ";
			List<Invoice> result = new List<Invoice>();

			foreach (DataRow row in invoices.Rows) {
				Invoice invoice = null;
				string uid = row["UID"] as string;
				string name = row["Name"] as string;
				string forename = row["Forename"] as string;
				string surname = row["Surname"] as string;
				string select = query + row["ID"].ToString();
				NpgsqlCommand command = new NpgsqlCommand(select, _connection);
				DataTable invoiceItems = Select(command);

				if (uid == null && name == null) {
					name = forename + " " + surname;
				}

				if (invoiceItems != null) {
					List<InvoiceItem> items = new List<InvoiceItem>();

					foreach (DataRow item in invoiceItems.Rows) {
						items.Add(new InvoiceItem((int)item["ID"], item["Name"] as string, item["Quantity"] as int?, item["UnitPrice"] as double?, item["VAT"] as double?));
					}

					invoice = new Invoice(
						(int)row["ID"], 
						name,
						row["Date"] as DateTime?,
						row["Maturity"] as DateTime?,
						row["Comment"] as string,
						row["Message"] as string,
						row["Type"] as string,
						items,
						(int)row["fk_Contact"],
						(bool)row["ReadOnly"]);
				} else {
					invoice = new Invoice(
						(int)row["ID"],
						name,
						row["Date"] as DateTime?,
						row["Maturity"] as DateTime?,
						row["Comment"] as string,
						row["Message"] as string,
						row["Type"] as string,
						new List<InvoiceItem>(),
						(int)row["fk_Contact"],
						(bool)row["ReadOnly"]);
				}

				result.Add(invoice);
			}

			return result;
		}

		public void UpsertContact(Contact contact)
		{
			if (contact.ID <= 0) {
				InsertContact(contact);
			} else {
				UpdateContact(contact);
			}
		}

		public void ExecuteSqlScript(string path)
		{
			FileInfo file = new FileInfo(path);
			string script = file.OpenText().ReadToEnd();
			NpgsqlCommand command = new NpgsqlCommand(script, _connection);
			command.ExecuteNonQuery();
		}

		private DataTable SelectCompanies()
		{
			NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Contact WHERE UID IS NOT NULL OR Name IS NOT NULL", _connection);
			command.Prepare();
			return Select(command);
		}

		private void InsertContact(Contact contact)
		{
			NpgsqlCommand command = new NpgsqlCommand("" +
				"INSERT INTO Contact" +
				"(UID,Name,Title,Forename,Surname,Suffix,BirthDate,Company,Street,StreetNumber,PostalCode,City)VALUES" +
				"(:uid,:name,:title,:forename,:surname,:suffix,:birth,:company,:street,:streetnumber,:zip,:city)",
				_connection);

			// add parameters and prepare query
			AddInsertParameters(command);
			command.Prepare();

			// add values
			SetInsertParameters(command, contact);

			if (InsertUpdateDelete(command) != 1) {
				throw new Exception();
			}
		}

		private void UpdateContact(Contact contact)
		{
			NpgsqlCommand command = new NpgsqlCommand("" +
				"UPDATE Contact SET " +
				"UID=:uid,Name=:name,Title=:title,Forename=:forename," +
				"Surname=:surname,Suffix=:suffix,BirthDate=:birth,Company=:company," +
				"Street=:street,StreetNumber=:streetnumber,PostalCode=:zip,City=:city " +
				"WHERE ID = :id",
				_connection);

			// add parameters and prepare query
			AddUpdateParameters(command);
			command.Prepare();

			// add values
			SetUpdateParameters(command, contact);

			if (InsertUpdateDelete(command) != 1) {
				throw new Exception();
			}
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
				"OR lower(City) LIKE lower(:city)",
				_connection);

			// add parameters and prepare query
			AddSelectParameters(command);
			command.Prepare();

			// add values
			SetSelectParameters(command, "%" + filter + "%");

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
				int? company = (row["Company"].GetType().Name == "DBNull" ? new Nullable<int>() : row["Company"] as int?);
				string street = (row["Street"].GetType().Name == "DBNull" ? null : row["Street"] as string);
				string number = (row["StreetNumber"].GetType().Name == "DBNull" ? null : row["StreetNumber"] as string);
				string code = (row["PostalCode"].GetType().Name == "DBNull" ? null : row["PostalCode"] as string);
				string city = (row["City"].GetType().Name == "DBNull" ? null : row["City"] as string);

				result.Add(new Contact(id, uid, name, title, forename, surname, suffix, birth, company, street, number, code, city));
			}

			return result;
		}

		private void AddUpdateParameters(NpgsqlCommand command)
		{
			AddInsertParameters(command);
			command.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
		}

		private void AddInsertParameters(NpgsqlCommand command)
		{
			AddSelectParameters(command);
			command.Parameters.Add("title", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("suffix", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("birth", NpgsqlTypes.NpgsqlDbType.Date);
			command.Parameters.Add("company", NpgsqlTypes.NpgsqlDbType.Integer);
			command.Parameters.Add("street", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("streetnumber", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("zip", NpgsqlTypes.NpgsqlDbType.Text);
		}

		private void AddSelectParameters(NpgsqlCommand command)
		{
			command.Parameters.Add("uid", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("forename", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("surname", NpgsqlTypes.NpgsqlDbType.Text);
			command.Parameters.Add("city", NpgsqlTypes.NpgsqlDbType.Text);
		}

		private void SetUpdateParameters(NpgsqlCommand command, Contact contact)
		{
			SetInsertParameters(command, contact);
			command.Parameters["id"].Value = contact.ID;
		}

		private void SetInsertParameters(NpgsqlCommand command, Contact contact)
		{
			command.Parameters["uid"].Value = contact.UID;
			command.Parameters["name"].Value = contact.Name;
			command.Parameters["title"].Value = contact.Title;
			command.Parameters["forename"].Value = contact.Forename;
			command.Parameters["surname"].Value = contact.Surname;
			command.Parameters["suffix"].Value = contact.Suffix;
			command.Parameters["birth"].Value = contact.BirthDate;
			command.Parameters["company"].Value = contact.Company;
			command.Parameters["street"].Value = contact.Street;
			command.Parameters["streetnumber"].Value = contact.StreetNumber;
			command.Parameters["zip"].Value = contact.PostalCode;
			command.Parameters["city"].Value = contact.City;
		}

		private void SetSelectParameters(NpgsqlCommand command, string filter)
		{
			command.Parameters["uid"].Value = filter;
			command.Parameters["name"].Value = filter;
			command.Parameters["forename"].Value = filter;
			command.Parameters["surname"].Value = filter;
			command.Parameters["city"].Value = filter;
		}

		/// <summary>
		/// Returns DataTable for given Sql Select Query.
		/// </summary>
		/// <param name="sql">Select query to execute</param>
		/// <returns>Selected rows or null if any error happened</returns>
		private DataTable Select(NpgsqlCommand command)
		{
			NpgsqlDataReader dbReader = null;
			DataTable dt = null;

			try {
				dbReader = command.ExecuteReader();
				dt = new DataTable();
				dt.Load(dbReader);
				return dt;
			} catch (Exception e) {
				if (dt != null) {
					DataRow[] rows = dt.GetErrors(); // Für Debugging
				}
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
			} catch (Exception e) {
				// TODO: specific exception handling
				return -1;
			}
		}
	}
}
