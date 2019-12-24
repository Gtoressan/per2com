namespace Per2com.DataModel.Entities
{
	public class RamType
	{
		public int Id { get; protected set; }

		public string Name { get; set; }

		public RamType(int id) => Id = id;

		public RamType() { }

		public override string ToString()
		{
			return Name;
		}
	}
}
