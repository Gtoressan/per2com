namespace Per2com.DataModel.Entities
{
	public class GraphicsCard
	{
		public int Id { get; protected set; }

		public int ManufacturerId { get; set; }

		public string Name { get; set; }

		public float Capacity { get; set; }

		public string MemoryType { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public GraphicsCard(int id) => Id = id;

		public GraphicsCard() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {MemoryType} {Name} {Capacity}";
		}
	}
}
