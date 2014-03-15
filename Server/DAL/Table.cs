using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
	abstract class Table : IDisposable, IDataAccess, IDataManipulation
	{
		protected DataTable DataTable { get; set; }

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

		public DataView GetDataView(string filter, string sort)
		{
			return new DataView(DataTable, filter, sort, DataViewRowState.None);
		}
	}
}
