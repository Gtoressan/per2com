namespace Per2com.DataModel.Entities
{
	public class PowerSupply
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public float Power { get; set; }

		public string FormFactor { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public PowerSupply() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {Name} {Power}";
		}
	}
}
