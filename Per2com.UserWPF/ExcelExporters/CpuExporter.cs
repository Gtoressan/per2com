using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class CpuExporter : ExcelExporter<Cpu>
	{
		public override void SaveExport(string path, Cpu[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "Процессоры",
				columnNames: new string[] {
					"Страна", "Производитель", "Наименование", "Сокет", "Число потоков", "Число ядер", "L1 (в Кб)", "L2 (в Кб)", "L3 (в Кб)", "Частота (в ГГц)"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.Name),
					array.Select(x => x.Socket.Name),
					array.Select(x => x.ThreadCount as object),
					array.Select(x => x.CoreCount as object),
					array.Select(x => x.L1 as object),
					array.Select(x => x.L2 as object),
					array.Select(x => x.L3 as object),
					array.Select(x => x.Frequency as object),
				}
			);
		}
	}
}
