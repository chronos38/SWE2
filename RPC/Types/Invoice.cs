using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Types
{
	[Serializable]
	public class Invoice
	{
		/// <summary>
		/// Es sei angemerkt dass diese ID der PK von den Invoices ist.
		/// </summary>
		public int ID { get; private set; }
		public string Name { get; private set; }
		public DateTime? Date { get; private set; }
		public DateTime? Maturity { get; private set; }
		public string Comment { get; private set; }
		public string Message { get; private set; }
		public string Type { get; private set; }
		public List<InvoiceItem> Items { get; private set; }
		public int Contact { get; private set; }

		public Invoice(int id, string name, DateTime? date, DateTime? maturity, string comment, string message, string type, List<InvoiceItem> items, int contact)
		{
			ID = id;
			Name = name;
			Date = date;
			Maturity = maturity;
			Comment = comment;
			Message = message;
			Type = type;
			Items = items;
			Contact = contact;
		}

		public Invoice(int id, string name, DateTime? date, DateTime? maturity, string comment, string message, string type, byte[] items, int contact)
		{
			// variables
			BinaryFormatter binaryFormatter = new BinaryFormatter();

			// setter
			ID = id;
			Name = name;
			Date = date;
			Maturity = maturity;
			Comment = comment;
			Message = message;
			Type = type;
			Contact = contact;

			// deserialize
			Items = (List<InvoiceItem>)binaryFormatter.Deserialize(new MemoryStream(items));
		}

		public Invoice(DataRow row)
		{
			FromDataRow(row);
		}

		public Invoice(object[] objects)
		{
			FromObjectArray(objects);
		}

		public DataRow ToDataRow(DataTable table)
		{
			// variables
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			DataRow result = table.NewRow();
			object date, maturity;

			if (Date == null) {
				date = DBNull.Value;
			} else {
				date = Date;
			}

			if (Maturity == null) {
				maturity = DBNull.Value;
			} else {
				maturity = Maturity;
			}

			// create entries
			result["ID"] = ID;
			result["Name"] = Name;
			result["Date"] = date;
			result["Maturity"] = maturity;
			result["Comment"] = Comment;
			result["Message"] = Message;
			result["Type"] = Type;
			result["Contact"] = Contact;
			
			// serialize
			binaryFormatter.Serialize(memoryStream, Items);
			result["Items"] = memoryStream.GetBuffer();

			// return
			return result;
		}

		public Invoice FromDataRow(DataRow row)
		{
			// variables
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			string date = row["Date"] as string;
			string maturity = row["Maturity"] as string;
			byte[] items = row["Items"] as byte[];

			// check dates
			if (date != null) {
				Date = DateTime.Parse(date, null);
			} else {
				Date = row["Date"] as DateTime?;
			}

			if (maturity != null) {
				Maturity = DateTime.Parse(maturity, null);
			} else {
				Maturity = row["Maturity"] as DateTime?;
			}

			// desirialize
			if (items != null) {
				Items = (List<InvoiceItem>)binaryFormatter.Deserialize(new MemoryStream(items));
			} else {
				Items = new List<InvoiceItem>();
			}

			// setters
			ID = Convert.ToInt32(row["ID"]);
			Name = row["Name"] as string;
			Comment = row["Comment"] as string;
			Message = row["Message"] as string;
			Type = row["Type"] as string;
			Contact = Convert.ToInt32(row["ID"]);

			return this;
		}

		public Invoice FromObjectArray(object[] objects)
		{
			// variables
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			string date = objects[2] as string;
			string maturity = objects[3] as string;
			byte[] items = objects[7] as byte[];

			// check dates
			if (date != null) {
				Date = DateTime.Parse(date, null);
			} else {
				Date = objects[2] as DateTime?;
			}

			if (maturity != null) {
				Maturity = DateTime.Parse(maturity, null);
			} else {
				Maturity = objects[3] as DateTime?;
			}

			// desirialize
			if (items != null) {
				Items = (List<InvoiceItem>)binaryFormatter.Deserialize(new MemoryStream(items));
			} else {
				Items = new List<InvoiceItem>();
			}

			// setters
			ID = Convert.ToInt32(objects[0]);
			Name = objects[1] as string;
			Comment = objects[4] as string;
			Message = objects[5] as string;
			Type = objects[6] as string;
			Contact = Convert.ToInt32(objects[8]);

			return this;
		}

		public static DataTable CreateTable()
		{
			// variables
			DataTable table = new DataTable();

			// add columns
			table.Columns.Add("ID");
			table.Columns.Add("Name");
			table.Columns.Add("Date");
			table.Columns.Add("Maturity");
			table.Columns.Add("Comment");
			table.Columns.Add("Message");
			table.Columns.Add("Type");
			table.Columns.Add("Items");
			table.Columns.Add("Contact");
			table.TableName = "Invoice";

			table.Columns["ID"].DataType = typeof(int);
			table.Columns["Date"].DataType = typeof(DateTime);
			table.Columns["Maturity"].DataType = typeof(DateTime);
			table.Columns["Items"].DataType = typeof(byte[]);
			table.Columns["Contact"].DataType = typeof(int);

			return table;
		}
	}
}
