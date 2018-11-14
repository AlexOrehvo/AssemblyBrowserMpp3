using AssemblyAnalyzer;
using AssemblyAnalyzer.Model;
using AssemblyBrowser.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.ViewModels
{
	class MainViewModel : INotifyPropertyChanged
	{
		private AsmAnalyzer asm;

		private string currNamespace;
		private string currEntity;

		public List<string> Namespaces => asm?.GetNamespaces();

		public string Namespace
		{
			get
			{
				return currNamespace;
			}
			set
			{
				currNamespace = value;
				OnPropertyChanged(nameof(Entities));
			}
		}

		public List<string> Entities
		{
			get
			{
				if (asm?.GetNamespace(Namespace) != null)
				{
					return asm?.GetNamespace(Namespace).EntityNames;
				} else
				{
					return null;
				}
			}
		}

		public string Entity
		{
			get { return currEntity; }
			set
			{
				currEntity = value;
				OnPropertyChanged(nameof(Fields));
				OnPropertyChanged(nameof(Properties));
				OnPropertyChanged(nameof(Methods));
			}
		}

		public List<string> Fields
		{
			get
			{
				if (Namespace != null && Entity != null)
				{
					return asm.GetNamespace(Namespace).GetEntity(Entity).FieldsNames;
				}
				return null;
			}
		}

		public List<string> Properties
		{
			get
			{
				if (Namespace != null && Entity != null)
				{
					return asm.GetNamespace(Namespace).GetEntity(Entity).PropertiesNames;
				}
				return null;
			}
		}

		public List<string> Methods
		{
			get
			{
				if (Namespace != null && Entity != null)
				{
					return asm.GetNamespace(Namespace).GetEntity(Entity).MethodsNames;
				}
				return null;
			}
		}

		public void ShutdownApp(object o)
		{
			Environment.Exit(0);
		}

		protected void LoadAssembly(object o)
		{
			IOService service = new IOService();
			string path = service.OpenFileDialog();
			asm = new AsmAnalyzer();
			asm.SetAssembly(service.GetAssembly(path));
			OnPropertyChanged(nameof(Namespaces));
			Namespace = null;
		}

		public MainViewModel()
		{
			asm = null;
			currEntity = null;
			currNamespace = null;
			ExitCommand = new BrowserCommand(ShutdownApp);
			LoadCommand = new BrowserCommand(LoadAssembly);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public BrowserCommand ExitCommand { get; protected set; }

		public BrowserCommand LoadCommand { get; protected set; }
	}
}
