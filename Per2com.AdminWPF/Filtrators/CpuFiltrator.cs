using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System.Linq;

namespace Per2com.AdminWPF.Filtrators
{
	public class CpuFiltrator : Filtrator<Cpu>
	{
		public string Manufacturer { get; set; }

		public string Socket { get; set; }

		public string Name { get; set; }

		public int ThreadCount { get; set; }

		public int CoreCount { get; set; }

		public float FrequencyMin { get; set; }

		public float FrequencyMax { get; set; }

		public override Cpu[] Filter(Cpu[] array)
		{
			var query = from i in array
						where string.IsNullOrWhiteSpace(Manufacturer) ? true : i.Manufacturer.Name.Contains(Manufacturer)
						where string.IsNullOrWhiteSpace(Socket) ? true : i.Socket.Name.Contains(Socket)
						where string.IsNullOrWhiteSpace(Name) ? true : i.Name.Contains(Name)
						where ThreadCount == 0 ? true : ThreadCount == i.ThreadCount
						where CoreCount == 0 ? true : CoreCount == i.CoreCount
						where i.Frequency > FrequencyMin
						where FrequencyMax <= 0 ? true : i.Frequency < FrequencyMax
						select i;

			return query.ToArray();
		}
	}
}
