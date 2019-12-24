using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class CpuDir : Directory<Cpu>
	{
		public CpuDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Cpu item)
		{
			Bridge.Execute(
				tag,
				"insert into Cpu (ManufacturerId, SocketId, Name, ThreadCount, CoreCount, L1, L2, L3, Frequency) values (@mId, @sId, @name, @thrCount, @crCount, @l1, @l2, @l3, @frequency)",
				("@mId", item.Manufacturer.Id),
				("@sId", item.Socket.Id),
				("@name", item.Name),
				("@thrCount", item.ThreadCount),
				("@crCount", item.CoreCount),
				("@l1", item.L1),
				("@l2", item.L2),
				("@l3", item.L3),
				("@frequency", item.Frequency)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Cpu where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Cpu item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Cpu set ManufacturerId = @mId, SocketId = @sId, Name = @name, ThreadCount = @thrCount, CoreCount = @crCount, L1 = @l1, L2 = @l2, L3 = @l3, Frequency = @frequency where Id = @id",
				("@id", keys[0]),
				("@mId", item.Manufacturer.Id),
				("@sId", item.Socket.Id),
				("@name", item.Name),
				("@thrCount", item.ThreadCount),
				("@crCount", item.CoreCount),
				("@l1", item.L1),
				("@l2", item.L2),
				("@l3", item.L3),
				("@frequency", item.Frequency)
			);
		}

		public override Cpu Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override Cpu[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Cpu.Id, Cpu.Name, ThreadCount, CoreCount, L1, L2, L3, Frequency, M.Id, M.Name, M.Country, S.Id, S.Name from Cpu join Manufacturer M on Cpu.ManufacturerId = M.Id join Socket S on Cpu.SocketId = S.Id")
						select new Cpu {
							Id = (int)i[0],
							Name = (string)i[1],
							ThreadCount = (int)i[2],
							CoreCount = (int)i[3],
							L1 = (float)i[4],
							L2 = (float)i[5],
							L3 = (float)i[6],
							Frequency = (float)i[7],
							Manufacturer = new Manufacturer((int)i[8]) {
								Name = (string)i[9],
								Country = (string)i[10]
							},
							Socket = new Socket {
								Id = (int)i[11],
								Name = (string)i[12]
							}
						};

			return query.ToArray();
		}
	}
}
