using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Per2com.UserWPF.ViewModels
{
	public abstract class ExcelExporter<T>
	{
		public abstract void SaveExport(string path, T[] array);

		protected virtual void Generate(string path, T[] array, string sheetName, string[] columnNames, IEnumerable<object>[] collections)
		{
			var package = new ExcelPackage();
			var sheet = package.Workbook.Worksheets.Add(sheetName);

			for (int i = 0; i < columnNames.Length; ++i) {
				sheet.Cells[$"{(char)(i + 65)}1"].Value = columnNames[i];
			}

			for (int i = 0; i < collections.Length; ++i) {
				var collection = collections[i].ToArray();

				for (int j = 0; j < collection.Length; ++j) {
					sheet.Cells[$"{(char)(i + 65)}{j + 2}"].Value = collection[j];
				}
			}

			package.SaveAs(new FileInfo(path));
		}
	}
}
