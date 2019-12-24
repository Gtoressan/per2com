namespace Per2com.DataModel.Entities
{
	public class Ram
	{
		public int Id { get; protected set; }

		public int ManufacturerId { get; set; }

		public int RamTypeId { get; set; }

		public float Capacity { get; set; }

		public float Frequency { get; set; }

		public int CL { get; set; }

		public string Name { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public RamType RamType { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public RamType[] RamTypes { get; set; }

		public Ram(int id) => Id = id;

		public Ram() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {Name} {Capacity} {RamType?.Name}";
		}
	}
}
