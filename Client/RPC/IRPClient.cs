using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

namespace Client.RPC
{
	public interface IRPClient
	{
		Task<RPResult> SendAndReceiveAsync(RPCall call);
	}
}
