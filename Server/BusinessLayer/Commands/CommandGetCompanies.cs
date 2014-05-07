using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;
using DataTransfer.Types;
using DataTransfer.Converter;
using System.Data;
using Server.DAL;

namespace Server.BusinessLayer.Commands
{
	public class CommandGetCompanies : ICommand
	{
		public CommandGetCompanies()
		{

		}

		public RPResult Execute(RPCall call)
		{
			List<Contact> companies = IDatabaseSingleton.Instance().GetCompanies();
			ContactListConverter conv = new ContactListConverter();

			RPResult ret = new RPResult();
			ret.dt = (DataTable)conv.ConvertTo(null, null, companies, typeof(DataTable));
			ret.dt.TableName = "Companies";
			return ret;
		}
	}
}
