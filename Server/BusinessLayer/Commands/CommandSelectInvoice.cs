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
	class CommandSelectInvoice : ICommand
	{
		public RPResult Execute(RPCall call)
		{
			if (call.procedureArgs == null || call.procedureArgs.Length < 1) {
				throw new InvalidOperationException();
			} else {
				RPResult result = new RPResult();
				List<Invoice> invoices = DatabaseFactory.Factory().SelectInvoice(Int32.Parse(call.procedureArgs[0]));

				DataTable table = Invoice.CreateTable();

				foreach (Invoice invoice in invoices) {
					table.Rows.Add(invoice.ToDataRow(table));
				}

				table.AcceptChanges();

				result.dt = table;
				result.dt.TableName = "Invoice";
				return result;
			}
		}
	}
}
