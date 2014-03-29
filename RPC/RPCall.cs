﻿using System;
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
		
		public RPCall()
		{
		}
		public RPCall(string procName, string[] args)
		{
			procedureName = procName;
			procedureArgs = args;
		}

		public RPCall(string procName)
		{
			procedureName = procName;
		}
	}
}
