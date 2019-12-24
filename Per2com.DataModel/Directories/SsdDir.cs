using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class SsdDir : Directory<Ssd>
	{
		public SsdDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Ssd item)
		{
			Bridge.Execute(
				tag,
				"insert into Ssd (ManufacturerId, Capacity, Name, FormFactor) values (@mId, @capacity, @name, @ff)",
				("@mId", item.Manufacturer.Id),
				("@capacity", item.Capacity),
				("@name", item.Name),
				("@ff", item.FormFactor)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Ssd where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Ssd item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Ssd set ManufacturerId = @mId, Capacity = @capacity, Name = @name, FormFactor = @ff where Id = @id",
				("@mId", item.Manufacturer.Id),
				("@capacity", item.Capacity),
				("@name", item.Name),
				("@ff", item.FormFactor),
				("@id", keys[0])
			);
		}

		public override Ssd Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override Ssd[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Ssd.Id, Capacity, Ssd.Name, FormFactor, M.Id, M.Name, M.Country from Ssd join Manufacturer M on Ssd.ManufacturerId = M.Id")
						select new Ssd((int)i[0]) {
							Capacity = (float)i[1],
							Name = (string)i[2],
							FormFactor = (string)i[3],
							ManufacturerId = (int)i[4],
							Manufacturer = new Manufacturer((int)i[4]) {
								Name = (string)i[5],
								Country = (string)i[6]
							}
						};

			return query.ToArray();
		}
	}
}
