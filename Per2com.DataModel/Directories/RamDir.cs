using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class RamDir : Directory<Ram>
	{
		public RamDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Ram item)
		{
			Bridge.Execute(
				tag,
				"insert into Ram (ManufacturerId, RamTypeId, Capacity, CL, Name, Frequency) values (@mId, @rtId, @capacity, @cl, @name, @frequency)",
				("@mId", item.Manufacturer.Id),
				("@rtId", item.RamType.Id),
				("@capacity", item.Capacity),
				("@cl", item.CL),
				("@name", item.Name),
				("@frequency", item.Frequency)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Ram where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Ram item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Ram set ManufacturerId = @mId, RamTypeId = @rtId, Name = @name, Capacity = @capacity, CL = @cl, Frequency = @frequency where Id = @id",
				("@mId", item.Manufacturer.Id),
				("@rtId", item.RamType.Id),
				("@capacity", item.Capacity),
				("@cl", item.CL),
				("@name", item.Name),
				("@frequency", item.Frequency),
				("@id", keys[0])
			);
		}

		public override Ram Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override Ram[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Ram.Id, ManufacturerId, RamTypeId, Capacity, Frequency, CL, M.Name, M.Country, RT.Name, Ram.Name from Ram join Manufacturer M on Ram.ManufacturerId = M.Id join RamType RT on Ram.RamTypeId = RT.Id")
						select new Ram((int)i[0]) {
							ManufacturerId = (int)i[1],
							RamTypeId = (int)i[2],
							Capacity = (float)i[3],
							Frequency = (float)i[4],
							CL = (int)i[5],
							Name = (string)i[9],
							Manufacturer = new Manufacturer((int)i[1]) {
								Country = (string)i[7],
								Name = (string)i[6]
							},
							RamType = new RamType((int)i[2]) {
								Name = (string)i[8]
							}
						};

			return query.ToArray();
		}
	}
}
