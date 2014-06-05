using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.RPC;
using System.IO;
using DataTransfer;
using System.Xml;
using System.Xml.Serialization;

namespace Tests.Server.RPC
{
	class MockHttpConnection : IHttpConnection
	{
		XmlSerializer _RPCallSerializer = new XmlSerializer(typeof(RPCall));
		string _response = null;
		int _statusCode;
		string _commandName = "CommandTest";

		public string CommandName
		{
			get { return _commandName; }
			set { _commandName = value; }
		}
		public string ResponseData
		{
			get { return _response; }
		}

		public int StatusCode
		{
			get { return _statusCode; }
		}

		public StreamReader GetClientData()
		{
			RPCall call = new RPCall(_commandName);
			MemoryStream stream = new MemoryStream();
			_RPCallSerializer.Serialize(stream, call);
			stream.Flush();
			stream.Seek(0, SeekOrigin.Begin);
			return new StreamReader(stream);
		}

		public void SendReponseData(byte[] response)
		{
			_response = Encoding.UTF8.GetString(response);
			_statusCode = 200;
		}

		public void SendErrorCode(int statusCode)
		{
			_statusCode = statusCode;
		}
	}
}
