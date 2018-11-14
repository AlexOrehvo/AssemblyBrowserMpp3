using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.Common
{
	class IOService
	{
		public string OpenFileDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Assembly (*.dll) | *.dll";
			if (openFileDialog.ShowDialog() == true)
			{
				return openFileDialog.FileName;
			}
			return null;
		}

		public Assembly GetAssembly(string path)
		{
			try
			{
				return Assembly.LoadFile(new FileInfo(path).FullName);
			}
			catch (BadImageFormatException)
			{
			}
			catch (FileLoadException)
			{
			}
			return null;
		}
	}
}
