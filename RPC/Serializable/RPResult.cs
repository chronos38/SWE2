using System;
using System.Data;
using System.Xml.Serialization;

namespace RPC
{
	[XmlRoot("RPResult")]
	[Serializable]
	public class RPResult
	{
		[XmlElement("id")]
		public int id;
		[XmlElement("count")]
		public int count;
		[XmlElement]
		public DataTable dt;
		[XmlElement("rowsAffected")]
		public int rowsAffected;
	}
}
