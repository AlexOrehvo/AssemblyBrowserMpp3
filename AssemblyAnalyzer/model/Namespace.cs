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

		private List<string> entityNames;

		public List<string> EntityNames
		{
			get { return entityNames; }
		}

		public Namespace(string name)
		{
			this.name = name;
			entities = new List<Entity>();
			entityNames = new List<string>();
		}

		public void AddEntity(Type type)
		{
			Entity entity = new Entity(type);
			entities.Add(entity);
			entityNames.Add(entity.GetName());
		}

		public Entity GetEntity(string name)
		{
			Entity entity = entities.Find(e => e.Name.Equals(name));
			return entity;
		}
			
	}
}
