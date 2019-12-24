using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class GraphicsCardDir : Directory<GraphicsCard>
	{
		public GraphicsCardDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, GraphicsCard item)
		{
			Bridge.Execute(
				tag,
				"insert into GraphicsCard (ManufacturerId, Name, Capacity, MemoryType) values (@mId, @name, @capacity, @memType)",
				("@mId", item.Manufacturer.Id),
				("@name", item.Name),
				("@capacity", item.Capacity),
				("@memType", item.MemoryType)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from GraphicsCard where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, GraphicsCard item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update GraphicsCard set ManufacturerId = @mId, Name = @name, Capacity = @capacity, MemoryType = @memType where Id = @id",
				("@id", keys[0]),
				("@mId", item.Manufacturer.Id),
				("@name", item.Name),
				("@capacity", item.Capacity),
				("@memType", item.MemoryType)
			);
		}

		public override GraphicsCard Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override GraphicsCard[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select GC.Id, ManufacturerId, GC.Name, Capacity, MemoryType, M.Name, M.Country from GraphicsCard GC join Manufacturer M on GC.ManufacturerId = M.Id")
						select new GraphicsCard((int)i[0]) {
							ManufacturerId = (int)i[1],
							Name = (string)i[2],
							Capacity = (float)i[3],
							MemoryType = (string)i[4],
							Manufacturer = new Manufacturer((int)i[1]) {
								Country = (string)i[6],
								Name = (string)i[5]
							}
						};

			return query.ToArray();
		}
	}
}
