using Per2com.DataModel.Entities;
using System;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class PowerSupplyDir : Directory<PowerSupply>
	{
		public PowerSupplyDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, PowerSupply item)
		{
			Bridge.Execute(
				tag,
				"insert into PowerSupply (ManufacturerId, Power, Name, FormFactor) values (@mId, @power, @name, @ff)",
				("@mId", item.Manufacturer.Id),
				("@name", item.Name),
				("@power", item.Power),
				("@ff", item.FormFactor)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from PowerSupply where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, PowerSupply item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update PowerSupply set ManufacturerId = @mId, Power = @power, Name = @name, FormFactor = @ff where Id = @id",
				("@mId", item.Manufacturer.Id),
				("@name", item.Name),
				("@power", item.Power),
				("@ff", item.FormFactor),
				("@id", keys[0])
			);
		}

		public override PowerSupply Find(string tag, params object[] keys)
		{
			throw new NotImplementedException();
		}

		public override PowerSupply[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select PowerSupply.Id, Power, PowerSupply.Name, FormFactor, M.Id, M.Name, M.Country from PowerSupply join Manufacturer M on PowerSupply.ManufacturerId = M.Id")
						select new PowerSupply {
							Id = (int)i[0],
							Power = (float)i[1],
							Name = (string)i[2],
							FormFactor = (string)i[3],
							Manufacturer = new Manufacturer((int)i[4]) {
								Name = (string)i[5],
								Country = (string)i[6]
							}
						};

			return query.ToArray();
		}
	}
}
