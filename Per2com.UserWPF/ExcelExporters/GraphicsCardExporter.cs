using Per2com.DataModel.Entities;
using Per2com.UserWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.UserWPF.ExcelExporters
{
	public class GraphicsCardExporter : ExcelExporter<GraphicsCard>
	{
		public override void SaveExport(string path, GraphicsCard[] array)
		{
			Generate(
				path: path,
				array: array,
				sheetName: "Видеокарты",
				columnNames: new string[] {
					"Страна", "Производитель", "Тип графической памяти", "Наименование", "Объем графической памяти (в Гб)"
				},
				collections: new IEnumerable<object>[] {
					array.Select(x => x.Manufacturer.Country),
					array.Select(x => x.Manufacturer.Name),
					array.Select(x => x.MemoryType),
					array.Select(x => x.Name),
					array.Select(x => x.Capacity as object)
				}
			);
		}
	}
}
