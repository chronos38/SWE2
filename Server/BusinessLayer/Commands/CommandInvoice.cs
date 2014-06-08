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
			List<Invoice> invoices = null;
			DataTable table = null;

			if ((call.Buffer == null || call.Buffer.Length < 1) && (call.procedureArgs == null || call.procedureArgs.Length < 1)) {
				throw new InvalidOperationException();
			} else if (call.Buffer != null) {
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream(call.Buffer);
				InvoiceSearchData data = (InvoiceSearchData)binaryFormatter.Deserialize(memoryStream);

				invoices = DatabaseFactory.Factory().SearchInvoices(data);
			} else {
				DateTime? from = call.procedureArgs[1] == "" ? new Nullable<DateTime>() : new Nullable<DateTime>(DateTime.Parse(call.procedureArgs[1]));
				DateTime? to = call.procedureArgs[2] == "" ? new Nullable<DateTime>() : new Nullable<DateTime>(DateTime.Parse(call.procedureArgs[2]));
				invoices = DatabaseFactory.Factory().SearchInvoices(Int32.Parse(call.procedureArgs[0]), from, to);
			}

			table = Invoice.CreateTable();

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
