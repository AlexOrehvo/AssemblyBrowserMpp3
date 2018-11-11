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

		public AsmAnalyzer(Assembly assembly)
		{
			this.assembly = assembly;
			this.namespaces = new List<Namespace>();
		}

		public List<Namespace> GetNamespaces() {
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
				}
			}
			return namespaces;
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
