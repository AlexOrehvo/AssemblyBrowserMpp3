using AssemblyAnalyzer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer
{
    public class AsmAnalyzer
    {
		private Assembly assembly;
		private List<Namespace> namespaces;
		private List<string> namespaceNames;

		public string Name
		{
			get { return assembly.FullName; }
		}

		public List<string> NamespaceNames
		{
			get;
		}

		public AsmAnalyzer()
		{
			this.assembly = null;
			this.namespaces = new List<Namespace>();
			this.namespaceNames = new List<string>();
		}

		public List<string> GetNamespaces() {
			List<Type> typeList = new List<Type>(GetLoadableTypes());

			foreach(Type type in typeList) {
				bool isNamespaceExist = false;
				string namespaceName = type.Namespace;
				foreach (Namespace nmspace in namespaces)
				{
					if (namespaceName.Equals(nmspace.Name))
					{
						isNamespaceExist = true;
						nmspace.AddEntity(type);
					}
				}
				if (!isNamespaceExist)
				{
					namespaces.Add(new Namespace(type.Namespace));
					namespaceNames.Add(type.Namespace);
				}
			}
			return namespaceNames;
		}

		public void SetAssembly(Assembly assembly)
		{
			this.assembly = assembly;
		}

		public Namespace GetNamespace(string namespaceName)
		{
			return namespaces.Find(ns => ns.Name.Equals(namespaceName));
		}

		private IEnumerable<Type> GetLoadableTypes()
		{
			if (assembly == null) throw new ArgumentNullException(nameof(assembly));
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
    }
}
