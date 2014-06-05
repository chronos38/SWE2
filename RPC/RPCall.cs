using System;
using System.Xml.Serialization;
using System.Data;

namespace DataTransfer
{
	[XmlRoot("RPCall")]
	[Serializable]
	public class RPCall
	{
		[XmlElement("procedureName")]
		public string procedureName { get; set; }
		[XmlArray("procedureArguments")]
		[XmlArrayItem("Argument")]
		public string[] procedureArgs { get; set; }
		public DataTable dt;
		[XmlElement("procedureArgument")]
		public byte[] Buffer { get; set; }
		
		public RPCall()
		{
			procedureName = "";
			procedureArgs = null;
			Buffer = null;
			dt = null;
		}

		public RPCall(string procName, string[] args)
		{
			procedureName = procName;
			procedureArgs = args;
			Buffer = null;
			dt = null;
		}

		public RPCall(string procName)
		{
			procedureName = procName;
			procedureArgs = null;
			Buffer = null;
			dt = null;
		}

		public RPCall(string procName, byte[] arg)
		{
			procedureName = procName;
			procedureArgs = null;
			Buffer = arg;
			dt = null;
		}
	}
}
