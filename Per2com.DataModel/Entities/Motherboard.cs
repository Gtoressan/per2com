namespace Per2com.DataModel.Entities
{
	public class Motherboard
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int RamCount { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public Socket Socket { get; set; }

		public Socket[] Sockets { get; set; }

		public RamType RamType { get; set; }

		public RamType[] RamTypes { get; set; }

		public Motherboard() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {Name} {Socket?.Name}";
		}
	}
}
