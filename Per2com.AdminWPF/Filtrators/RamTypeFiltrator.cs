using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class RamTypeFiltrator : Filtrator<RamType>
	{
		public string Name { get; set; }

		public override RamType[] Filter(RamType[] array)
		{
			var query = from i in array
						where string.IsNullOrWhiteSpace(Name) ? true : i.Name.Contains(Name)
						select i;

			return query.ToArray();
		}
	}
}
