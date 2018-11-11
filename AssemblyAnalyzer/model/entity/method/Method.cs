using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer.Model
{
	public class Method
	{
		private string name;
		private MethodInfo methodInfo;

		public Method(MethodInfo methodInfo)
		{
			this.methodInfo = methodInfo;
		}

		public string GetName()
		{
			name += GetAccessModifier();
			if (methodInfo.IsStatic)
			{
				name += " static";
			}
			name += " " + methodInfo.ReturnType.Name;
			name += " " + methodInfo.Name;
			name += " " + GetIncomingValues();
			return name;
		}

		private string GetAccessModifier()
		{
			if (methodInfo.IsPrivate)
			{
				return "private";
			}
			if (methodInfo.IsPublic)
			{
				return "public";
			}
			return "";
		}

		private string GetIncomingValues()
		{
			string str = "(";
			foreach (var param in methodInfo.GetParameters())
			{
				str += param.ParameterType.Name + " " + param.Name + ", ";
			}
			str += ");";
			return str;
		}
	}
}
