using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class MotherboardFiltrator : Filtrator<Motherboard>
	{
		public string Manufacturer { get; set; }

		public string Socket { get; set; }

		public string RamType { get; set; }

		public string Name { get; set; }

		public int RamCount { get; set; }

		public override Motherboard[] Filter(Motherboard[] array)
		{
			var query = from i in array
						where string.IsNullOrWhiteSpace(Manufacturer) ? true : i.Manufacturer.Name.Contains(Manufacturer)
						where string.IsNullOrWhiteSpace(Socket) ? true : i.Socket.Name.Contains(Socket)
						where string.IsNullOrWhiteSpace(RamType) ? true : i.RamType.Name.Contains(RamType)
						where string.IsNullOrWhiteSpace(Name) ? true : i.Name.Contains(Name)
						where RamCount <= 0 ? true : RamCount == i.RamCount
						select i;

			return query.ToArray();
		}
	}
}
