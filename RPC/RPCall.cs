using System;
using System.Xml.Serialization;

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
	}
}
