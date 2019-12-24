using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class SocketFiltrator : Filtrator<Socket>
	{
		public string Name { get; set; }

		public override Socket[] Filter(Socket[] array)
		{
			var query = from i in array
						where string.IsNullOrWhiteSpace(Name) ? true : i.Name.Contains(Name)
						select i;

			return query.ToArray();
		}
	}
}
