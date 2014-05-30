using Client.Data;
using DataTransfer;
using DataTransfer.Types;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer.Commands
{
	public class CommandInvoice : ICommand
	{
		public CommandInvoice()
		{
		
		}

		public RPResult Execute(RPCall call)
		{
			if (call.Buffer == null || call.Buffer.Length < 1) {
				throw new InvalidOperationException();
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(call.Buffer);
			InvoiceSearchData data = (InvoiceSearchData)binaryFormatter.Deserialize(memoryStream);

			List<Invoice> invoices = DatabaseSingleton.Instance().SearchInvoices(data);
			DataTable table = Invoice.CreateTable();

			foreach (Invoice invoice in invoices) {
				table.Rows.Add(invoice.ToDataRow(table));
			}

			table.AcceptChanges();

			RPResult retVal = new RPResult();
			retVal.dt = table;
			retVal.dt.TableName = "Invoices";
			return retVal;
		}
	}
}
