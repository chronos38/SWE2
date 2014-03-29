using System;
using System.Data;
using System.Xml.Serialization;

namespace DataTransfer
{
	[XmlRoot("RPResult")]
	[Serializable]
	public class RPResult
	{
		[XmlElement("count")]
		public int count;
		[XmlElement]
		public DataTable dt;
		[XmlElement("rowsAffected")]
		public int rowsAffected;
	}
}
