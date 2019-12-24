using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class PowerSupplyFiltrator : Filtrator<PowerSupply>
	{
		public string Manufacturer { get; set; }

		public string Name { get; set; }

		public int MinPower { get; set; }

		public int MaxPower { get; set; }

		public string FormFactor { get; set; }

		public override PowerSupply[] Filter(PowerSupply[] array)
		{
			var query = from i in array
						where Manufacturer is null ? true : i.Manufacturer.Name.Contains(Manufacturer)
						where Name is null ? true : i.Name.Contains(Name)
						where i.Power > MinPower
						where MaxPower <= 0 || MaxPower <= MinPower ? true : i.Power < MaxPower
						where FormFactor is null ? true : i.FormFactor.Contains(FormFactor)
						select i;

			return query.ToArray();
		}
	}
}
