using AssemblyAnalyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Assembly asm = GetAssembly("Faker.dll");

			AsmAnalyzer asmAnalizer = new AsmAnalyzer(asm);

			foreach (var nmsp in asmAnalizer.GetNamespaces())
			{
				Console.WriteLine(nmsp.Name);
				foreach(var entity in nmsp.Entities)
				{
					Console.WriteLine("---" + entity.GetName());
					foreach(var field in entity.Fields)
					{
						Console.WriteLine("------" + field.GetName());
					}
					foreach (var property in entity.Properties)
					{
						Console.WriteLine("------" + property.GetName());
					}
					foreach (var method in entity.Methods)
					{
						string str = method.GetName();
						Console.WriteLine("------" + str);
					}
					Console.WriteLine();
				}
			}

			Console.WriteLine("Hello world!");
		}

		private static Assembly GetAssembly(string path)
		{
			try
			{
				return Assembly.LoadFile(new FileInfo(path).FullName);
			}
			catch (BadImageFormatException)
			{ Console.WriteLine("111"); }
			catch (FileLoadException)
			{ Console.WriteLine("222"); }
			return null;
		}
	}
}
