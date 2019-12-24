using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class SsdExporter : ExcelExporter<Ssd>
	{
		public override void SaveExport(string path, Ssd[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "SSD",
				columnNames: new string[] {
					"Страна", "Производитель", "Наименование", "Объем (в Гб)", "Форм-фактор"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.Name),
					array.Select(x => x.Capacity as object),
					array.Select(x => x.FormFactor)
				}
			);
		}
	}
}
