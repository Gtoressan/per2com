namespace Per2com.DataModel.Entities
{
	public class Socket
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Socket() { }

		public override string ToString()
		{
			return Name;
		}
	}
}
