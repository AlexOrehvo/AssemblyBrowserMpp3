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

		public string Name
		{
			get { return name; }
		}
	
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

		public List<string> FieldsNames
		{
			get;
		}
		public List<string> PropertiesNames
		{
			get;
		}
		public List<string> MethodsNames
		{
			get;
		}

		public Entity(Type type)
		{
			this.type = type;
			this.Fields = new List<Field>();
			this.Properties = new List<Property>();
			this.Methods = new List<Method>();

			this.FieldsNames = new List<string>();
			this.PropertiesNames = new List<string>();
			this.MethodsNames = new List<string>();

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
				Field field = new Field(fieldInfo);
				Fields.Add(field);
				FieldsNames.Add(field.GetName());
			}
		}

		private void SetProperties()
		{
			foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance |
				BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Property property = new Property(propertyInfo);
				Properties.Add(property);
				PropertiesNames.Add(property.GetName());
			}
		}

		private void SetMethods()
		{
			foreach (var methodInfo in type.GetMethods())
			{
				Method method = new Method(methodInfo);
				Methods.Add(method);
				MethodsNames.Add(method.GetName());
			}
		}
	}
}
