using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class HddFiltrator : Filtrator<Hdd>
	{
		public string Manufacturer { get; set; }

		public string Name { get; set; }

		public float CapacityMin { get; set; }

		public float CapacityMax { get; set; }

		public int RotatingPerMinuteMin { get; set; }

		public int RotatingPerMinuteMax { get; set; }

		public string FormFactor { get; set; }

		public override Hdd[] Filter(Hdd[] array)
		{
			var query = from i in array
						where Manufacturer is null ? true : i.Manufacturer.Name.Contains(Manufacturer)
						where Name is null ? true : i.Name.Contains(Name)
						where i.Capacity > CapacityMin
						where CapacityMax <= 0 ? true : i.Capacity < CapacityMax
						where i.RotatingPerMinute > RotatingPerMinuteMin
						where RotatingPerMinuteMax <= 0 ? true : i.RotatingPerMinute < RotatingPerMinuteMax
						where FormFactor is null ? true : i.FormFactor.Contains(FormFactor)
						select i;

			return query.ToArray();
		}
	}
}
