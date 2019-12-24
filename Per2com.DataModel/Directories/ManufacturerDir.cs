using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class ManufacturerDir : Directory<Manufacturer>
	{
		public ManufacturerDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Manufacturer item)
		{
			Bridge.Execute(
				tag,
				"insert into Manufacturer (Country, Name) values (@country, @name)",
				("@country", item.Country),
				("@name", item.Name)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Manufacturer where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Manufacturer item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Manufacturer set Country = @country, Name = @name where Id = @id",
				("@country", item.Country),
				("@id", keys[0]),
				("@name", item.Name)
			);
		}

		public override Manufacturer Find(string tag, params object[] keys)
		{
			var values = Bridge.Select(
				tag,
				"select Country, Id, Name from Manufacturer where Id = @id"
			).FirstOrDefault();

			if (values.Length == 3) {
				return new Manufacturer((int)values[1]) {
					Country = (string)values[0],
					Name = (string)values[2]
				};
			}

			return null;
		}

		public override Manufacturer[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Country, Id, Name from Manufacturer")
						select new Manufacturer((int)i[1]) {
							Country = (string)i[0],
							Name = (string)i[2]
						};

			return query.ToArray();
		}
	}
}
