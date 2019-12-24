using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class RamFiltrator : Filtrator<Ram>
	{
		public string Manufacturer { get; set; }

		public string RamType { get; set; }

		public string Name { get; set; }

		public float CapacityMin { get; set; }

		public float CapacityMax { get; set; }

		public override Ram[] Filter(Ram[] array)
		{
			var query = from i in array
						where string.IsNullOrWhiteSpace(Manufacturer) ? true : i.Manufacturer.Name.Contains(Manufacturer)
						where string.IsNullOrWhiteSpace(RamType) ? true : i.RamType.Name.Contains(RamType)
						where string.IsNullOrWhiteSpace(Name) ? true : i.Name.Contains(Name)
						where i.Capacity > CapacityMin
						where CapacityMax <= 0 ? true : i.Capacity < CapacityMax
						select i;

			return query.ToArray();
		}
	}
}
