namespace Per2com.DataModel.Entities
{
	public class Hdd
	{
		public int Id { get; protected set; }

		public int ManufacturerId { get; set; }

		public int RotatingPerMinute { get; set; }

		public float Capacity { get; set; }

		public string Name { get; set; }

		public string FormFactor { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public Hdd(int id) => Id = id;

		public Hdd() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {Name} {Capacity} {FormFactor}";
		}
	}
}
