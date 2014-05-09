using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.RPC;
using DataTransfer;

namespace Tests.Client.RPC
{
	class MockRPClient  : IRPClient
	{
		TestHelper th = new TestHelper();

		public Task<RPResult> SendAndReceiveAsync(RPCall call)
		{
			RPResult result = new RPResult();
			switch(call.procedureName) {
				case "CommandContact":
					result.dt = th.GetTestPersonDataTable();
					break;
				case "CommandUpsert":
					result.success = 1;
					break;
				case "CommandGetCompanies":
					result.dt = th.GetTestCompanyDataTable();
					break;
				default:
					break;
			}
			var tcs = new TaskCompletionSource<RPResult>();
			tcs.SetResult(result);
			return tcs.Task;
		}
	}
}
