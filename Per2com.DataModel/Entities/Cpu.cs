namespace Per2com.DataModel.Entities
{
	public class Cpu
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public float Frequency { get; set; }

		public int ThreadCount { get; set; }

		public int CoreCount { get; set; }

		public float L1 { get; set; }

		public float L2 { get; set; }

		public float L3 { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public Manufacturer[] Manufacturers { get; set; }

		public Socket Socket { get; set; }

		public Socket[] Sockets { get; set; }

		public Cpu() { }

		public override string ToString()
		{
			return $"{Manufacturer?.Name} {Name} {Frequency}";
		}
	}
}
