using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer.Model
{
	public class Field
	{
		private string fieldName;
		private FieldInfo fieldInfo;

		public Field(FieldInfo fieldInfo)
		{
			this.fieldInfo = fieldInfo;
		}

		public string GetName()
		{
			fieldName = GetAccessModifier();
			if (fieldInfo.IsStatic)
			{
				fieldName += " static";
			}
			fieldName += " " + fieldInfo.FieldType.Name;
			fieldName += " " + fieldInfo.Name;
			return fieldName;
		}

		private string GetAccessModifier()
		{
			if (fieldInfo.IsPrivate)
			{
				return "private";
			}
			if (fieldInfo.IsPublic)
			{
				return "public";
			}
			return "";
		}
	}
}
