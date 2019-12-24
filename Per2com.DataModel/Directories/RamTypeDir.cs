using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class RamTypeDir : Directory<RamType>
	{
		public RamTypeDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, RamType item)
		{
			Bridge.Execute(
				tag,
				"insert into RamType (Name) values (@name)",
				("@name", item.Name)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from RamType where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, RamType item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update RamType set Name = @name where Id = @id",
				("@id", keys[0]),
				("@name", item.Name)
			);
		}

		public override RamType Find(string tag, params object[] keys)
		{
			var values = Bridge.Select(
				tag,
				"select Id, Name from RamType where Id = @id"
			).FirstOrDefault();

			if (values.Length == 3) {
				return new RamType((int)values[0]) {
					Name = (string)values[1]
				};
			}

			return null;
		}

		public override RamType[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Id, Name from RamType")
						select new RamType((int)i[0]) {
							Name = (string)i[1]
						};

			return query.ToArray();
		}
	}
}
