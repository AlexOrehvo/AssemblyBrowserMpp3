using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer.Model
{
	public class Namespace
	{
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private List<Entity> entities;

		public List<Entity> Entities
		{
			get { return entities; }
			set { entities = value; }
		}

		public Namespace(string name)
		{
			this.name = name;
			entities = new List<Entity>();
		}

		public void AddEntity(Type type)
		{
			entities.Add(new Entity(type));
		}
	}
}
