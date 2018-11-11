using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer.Model
{
	public class Entity
	{
		private string name;
		private Type type;

		public List<Field> Fields
		{
			get;
		}
		public List<Property> Properties
		{
			get;
		}
		public List<Method> Methods
		{
			get;
		}

		public Entity(Type type)
		{
			this.type = type;
			this.Fields = new List<Field>();
			this.Properties = new List<Property>();
			this.Methods = new List<Method>();
			SetFields();
			SetProperties();
			SetMethods();
		}

		public string GetName()
		{
			if (type.IsPublic)
			{
				name = "public";
			}
			if (type.IsAbstract)
			{
				name += " abstract";
			}
			if (type.IsClass)
			{
				name += " class";
			}
			if (type.IsInterface)
			{
				name += " interface";
			}
			name += " " + type.Name;
			return name;
		}

		private void  SetFields()
		{
			foreach (var fieldInfo in type.GetFields(BindingFlags.Instance | 
				BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Fields.Add(new Field(fieldInfo));
			}
		}

		private void SetProperties()
		{
			foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance |
				BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Properties.Add(new Property(propertyInfo));
			}
		}

		private void SetMethods()
		{
			foreach (var methodInfo in type.GetMethods())
			{
				Methods.Add(new Method(methodInfo));
			}
		}
	}
}
