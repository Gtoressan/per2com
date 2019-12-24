using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.DataModel.Directories
{
	public class SocketDir : Directory<Socket>
	{
		public SocketDir(Bridge bridge) : base(bridge) { }

		public override void Add(string tag, Socket item)
		{
			Bridge.Execute(
				tag,
				"insert into Socket (Name) values (@name)",
				("@name", item.Name)
			);
		}

		public override void Drop(string tag, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"delete from Socket where Id = @id",
				("@id", keys[0])
			);
		}

		public override void Edit(string tag, Socket item, params object[] keys)
		{
			Bridge.Execute(
				tag,
				"update Socket set Name = @name where Id = @id",
				("@id", keys[0]),
				("@name", item.Name)
			);
		}

		public override Socket Find(string tag, params object[] keys)
		{
			var values = Bridge.Select(
				tag,
				"select Id, Name from Socket where Id = @id"
			).FirstOrDefault();

			if (values.Length == 3) {
				return new Socket {
					Id = (int)values[0],
					Name = (string)values[1]
				};
			}

			return null;
		}

		public override Socket[] Get(string tag)
		{
			var query = from i in Bridge.Select(tag, "select Id, Name from Socket")
						select new Socket {
							Id = (int)i[0],
							Name = (string)i[1]
						};

			return query.ToArray();
		}
	}
}
