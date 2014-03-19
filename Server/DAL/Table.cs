using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	class Table : IDisposable, IDataAccess, IDataManipulation
	{
		public DataTable DataTable { get; private set; }
		private string TableName { get; set; }

		private Table(string table)
		{
			// variables
			Database db = Database.Instance;
			string query = String.Format("SELECT * FROM {0}", table);

			// check database state
			if (!db.IsConnected()) {
				throw new ApplicationException("Database is not connected.");
			}

			// select data table
			DataTable = db.Select(query);

			// check if all went successful
			if (DataTable == null) {
				throw new ApplicationException(query);
			}

			// set table name
			TableName = table;
		}

		private void Insert(DataRow row)
		{
			throw new NotImplementedException();
		}

		private void Update(DataRow row)
		{
			throw new NotImplementedException();
		}

		private void Delete(object id)
		{
			// variables
			string query = String.Format("DELETE FROM {0} WHERE ID = {1}", TableName, id);
			Database db = Database.Instance;

			// delete
			if (db.InsertUpdateDelete(query) != 1) {
				throw new ApplicationException(query);
			}
		}

		public void Dispose()
		{
			DataTable.Dispose();
		}

		public DataRowCollection Rows
		{
			get
			{
				return DataTable.Rows;
			}
		}

		public DataColumn[] PrimaryKeys
		{
			get
			{
				return DataTable.PrimaryKey;
			}
		}

		public object Compute(string expression, string filter)
		{
			return DataTable.Compute(expression, filter);
		}

		public DataRow[] Select()
		{
			return DataTable.Select();
		}

		public DataRow[] Select(string filter)
		{
			return DataTable.Select(filter);
		}

		public DataRow[] Select(string filter, string sort)
		{
			return DataTable.Select(filter, sort);
		}

		public void AcceptChanges()
		{
			// variables
			Database db = Database.Instance;
			DataTable changes = DataTable.GetChanges();
			DataRowCollection rows = changes.Rows;

			// check database state
			if (!db.IsConnected()) {
				throw new ApplicationException("Database is not connected.");
			}

			// add changes to database
			foreach (DataRow row in rows) {
				switch (row.RowState) {
					case DataRowState.Added:
						Insert(row);
						break;
					case DataRowState.Deleted:
						Delete(row["ID"]); // it is assumed that every table has an ID
						break;
					case DataRowState.Modified:
						Update(row);
						break;
					default:
						rows.Remove(row);
						break;
				}
			}

			// accept changes
			DataTable.AcceptChanges();
		}

		public void Add(DataRow row)
		{
			DataTable.Rows.Add(row);
		}

		public void Add(DataRow[] rows)
		{
			foreach (var row in rows) {
				DataTable.Rows.Add(row);
			}
		}

		public void ImportRow(DataRow row)
		{
			DataTable.ImportRow(row);
		}

		public DataRow NewRow()
		{
			return DataTable.NewRow();
		}

		public DataView GetDataView()
		{
			return new DataView(DataTable);
		}

		public DataView GetDataView(string filter, string sort, DataViewRowState state = DataViewRowState.None)
		{
			return new DataView(DataTable, filter, sort, state);
		}

		public static Table AdditionalAddress
		{
			get
			{
				return new Table("AdditionalAddress");
			}
		}

		public static Table Address
		{
			get
			{
				return new Table("Address");
			}
		}

		public static Table CompanyData
		{
			get
			{
				return new Table("CompanyData");
			}
		}

		public static Table Contact
		{
			get
			{
				return new Table("Contact");
			}
		}

		public static Table Invoice
		{
			get
			{
				return new Table("Invoice");
			}
		}

		public static Table InvoiceItem
		{
			get
			{
				return new Table("InvoiceItem");
			}
		}

		public static Table InvoicePosition
		{
			get
			{
				return new Table("InvoicePosition");
			}
		}

		public static Table InvoiceType
		{
			get
			{
				return new Table("InvoiceType");
			}
		}

		public static Table PersonData
		{
			get
			{
				return new Table("PersonData");
			}
		}

		public static Table ValueAddedTax
		{
			get
			{
				return new Table("ValueAddedTax");
			}
		}
	}
}
