using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class PowerSupplyExporter : ExcelExporter<PowerSupply>
	{
		public override void SaveExport(string path, PowerSupply[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "Блоки питания",
				columnNames: new string[] {
					"Страна", "Производитель", "Наименование", "Мощность (Вт)", "Форм-фактор"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.Name),
					array.Select(x => x.Power as object),
					array.Select(x => x.FormFactor)
				}
			);
		}
	}
}
