using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class ManufacturerFiltrator : Filtrator<Manufacturer>
	{
		public string Country { get; set; }

		public string Name { get; set; }

		public override Manufacturer[] Filter(Manufacturer[] array)
		{
			var query = from i in array
						where string.IsNullOrWhiteSpace(Country) ? true : i.Country.Contains(Country)
						where string.IsNullOrWhiteSpace(Name) ? true : i.Name.Contains(Name)
						select i;

			return query.ToArray();
		}
	}
}
