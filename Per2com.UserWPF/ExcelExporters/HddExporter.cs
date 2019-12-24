using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class HddExporter : ExcelExporter<Hdd>
	{
		public override void SaveExport(string path, Hdd[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "HDD",
				columnNames: new string[] {
					"Страна", "Производитель", "Наименование", "Объем (в Гб)", "Скорость вращения диска (об/мин)", "Форм-фактор"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.Name),
					array.Select(x => x.Capacity as object),
					array.Select(x => x.RotatingPerMinute as object),
					array.Select(x => x.FormFactor)
				}
			);
		}
	}
}
