using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class HddDir : Directory<Hdd>
	{
		public HddDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Hdd item)
		{
			Bridge.Execute(
				tag,
				"insert into Hdd (ManufacturerId, RotatingPerMinute, Capacity, Name, FormFactor) values (@mId, @rpm, @capacity, @name, @ff)",
				("@mId", item.Manufacturer.Id),
				("@rpm", item.RotatingPerMinute),
				("@capacity", item.Capacity),
				("@name", item.Name),
				("@ff", item.FormFactor)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Hdd where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Hdd item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Hdd set ManufacturerId = @mId, RotatingPerMinute = @rpm, Capacity = @capacity, Name = @name, FormFactor = @ff where Id = @id",
				("@mId", item.Manufacturer.Id),
				("@rpm", item.RotatingPerMinute),
				("@capacity", item.Capacity),
				("@name", item.Name),
				("@ff", item.FormFactor),
				("@id", keys[0])
			);
		}

		public override Hdd Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override Hdd[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Hdd.Id, RotatingPerMinute, Capacity, Hdd.Name, FormFactor, M.Id, M.Name, M.Country from Hdd join Manufacturer M on Hdd.ManufacturerId = M.Id")
						select new Hdd((int)i[0]) {
							RotatingPerMinute = (int)i[1],
							Capacity = (float)i[2],
							Name = (string)i[3],
							FormFactor = (string)i[4],
							ManufacturerId = (int)i[5],
							Manufacturer = new Manufacturer((int)i[5]) {
								Name = (string)i[6],
								Country = (string)i[7]
							}
						};

			return query.ToArray();
		}
	}
}
