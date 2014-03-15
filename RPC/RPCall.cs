using System;
using System.Xml.Serialization;

namespace RPC
{
	[XmlRoot("RPCall")]
	[Serializable]
	public class RPCall
	{
		[XmlElement("id")]
		public int id { get; set; }
		[XmlElement("procedureName")]
		public string procedureName { get; set; }
		[XmlArray("procedureArguments")]
		[XmlArrayItem("Argument")]
		public string[] procedureArgs { get; set; }
	}
}
