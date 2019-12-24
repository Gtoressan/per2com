using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class MotherboardDir : Directory<Motherboard>
	{
		public MotherboardDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Motherboard item)
		{
			Bridge.Execute(
				tag,
				"insert into Motherboard (ManufacturerId, SocketId, RamTypeId, Name, RamCount) values (@mId, @sId, @rtId, @name, @rmCount)",
				("@mId", item.Manufacturer.Id),
				("@sId", item.Socket.Id),
				("@rtId", item.RamType.Id),
				("@name", item.Name),
				("@rmCount", item.RamCount)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Motherboard where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Motherboard item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Motherboard set ManufacturerId = @mId, SocketId = @sId, RamTypeId = @rtId, Name = @name, RamCount = @rmCount where Id = @id",
				("@id", keys[0]),
				("@mId", item.Manufacturer.Id),
				("@sId", item.Socket.Id),
				("@rtId", item.RamType.Id),
				("@name", item.Name),
				("@rmCount", item.RamCount)
			);
		}

		public override Motherboard Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override Motherboard[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select MB.Id, MB.Name, RamCount, M.Id, M.Country, M.Name, S.Id, S.Name, RT.Id, RT.Name from Motherboard MB join Manufacturer M on MB.ManufacturerId = M.Id join Socket S on MB.SocketId = S.Id join RamType RT on MB.Id = RT.Id")
						select new Motherboard {
							Id = (int)i[0],
							Name = (string)i[1],
							RamCount = (int)i[2],
							Manufacturer = new Manufacturer((int)i[3]) {
								Country = (string)i[4],
								Name = (string)i[5]
							},
							Socket = new Socket {
								Id = (int)i[6],
								Name = (string)i[7]
							},
							RamType = new RamType((int)i[8]) {
								Name = (string)i[9]
							}
						};

			return query.ToArray();
		}
	}
}
