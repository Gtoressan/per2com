using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class MotherboardExporter : ExcelExporter<Motherboard>
	{
		public override void SaveExport(string path, Motherboard[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "Материнские платы",
				columnNames: new string[] {
					"Страна", "Производитель",  "Сокет", "Тип поддерживаемого ОЗУ", "Наименование", "Число плашек для ОЗУ"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.Socket.Name),
					array.Select(x => x.RamType.Name),
					array.Select(x => x.Name),
					array.Select(x => x.RamCount as object),
				}
			);
		}
	}
}
