using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RPC
{
	interface IHttpConnection
	{
		StreamReader GetClientData();
		void SendReponseData(byte[] response);
		void SendErrorCode(int statusCode);
	}
}
