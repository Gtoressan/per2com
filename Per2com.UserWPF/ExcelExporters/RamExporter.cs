using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class RamExporter : ExcelExporter<Ram>
	{
		public override void SaveExport(string path, Ram[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "ОЗУ",
				columnNames: new string[] {
					"Страна", "Производитель", "ТИП ОЗУ", "Наименование", "Объем (в Гб)", "Частота", "CAS Latency"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.RamType.Name),
					array.Select(x => x.Name),
					array.Select(x => x.Capacity as object),
					array.Select(x => x.Frequency as object),
					array.Select(x => x.CL as object)
				}
			);
		}
	}
}
