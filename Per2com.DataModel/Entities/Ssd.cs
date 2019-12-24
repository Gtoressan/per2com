namespace Per2com.DataModel.Entities
{
	public class Ssd
	{
		public int Id { get; protected set; }

		public int ManufacturerId { get; set; }

		public float Capacity { get; set; }

		public string Name { get; set; }

		public string FormFactor { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public Ssd(int id) => Id = id;

		public Ssd() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {Name} {Capacity} {FormFactor}";
		}
	}
}
