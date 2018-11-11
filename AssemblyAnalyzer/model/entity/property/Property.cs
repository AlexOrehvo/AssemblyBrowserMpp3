using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer.Model
{
	public class Property
	{
		private string name;
		private PropertyInfo propertyInfo;

		public Property(PropertyInfo propertyInfo)
		{
			this.propertyInfo = propertyInfo;
		}

		public string GetName()
		{
			name += propertyInfo.PropertyType.Name;
			name += " " + propertyInfo.Name;
			name += "{";
			if (propertyInfo.CanRead)
			{
				name += " get; ";
			}
			if (propertyInfo.CanWrite)
			{
				name += " set; ";
			}
			name += "}";
			return name;
		}
	}
}
