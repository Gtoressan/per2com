namespace Per2com.DataModel.Entities
{
	public class Manufacturer
	{
		public int Id { get; protected set; }

		public string Name { get; set; }

		public string Country { get; set; }

		public Manufacturer(int id) => Id = id;

		public Manufacturer() { }

		public override string ToString()
		{
			return $"{Name} ({Country})";
		}
	}
}
