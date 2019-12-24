using Microsoft.Win32;
using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using System.Windows.Controls;
using System.Windows.Input;

using static System.Environment;
using static System.Environment.SpecialFolder;

namespace Per2com.UserWPF.ViewModels.BrowsedPages
{
	class IndexPageVM<T> : AdminWPF.ViewModels.BrowsedPages.IndexPageVM<T>
	{
		public ExcelExporter<T> Exporter { get; set; }

		public ICommand Export => new CommandDelegate {
			ExecuteDelegate = (x) => ExportToExcel()
		};

		public IndexPageVM() { }

		public Page Bind(Page page, BaseVM owner, Bridge bridge, ResultHandler handler, ExcelExporter<T> exporter, Directory<T> directory, Validator<T> validator, Filtrator<T> filtrator)
		{
			Exporter = exporter;
			return Bind(page, owner, bridge, handler, directory, validator, filtrator);
		}

		public void ExportToExcel()
		{
			var sfd = new SaveFileDialog() {
				InitialDirectory = GetFolderPath(Desktop),
				CheckPathExists = true,
				Title = "Экспорт данных",
				Filter = "Excel Files (*.xlsx) | *.xlsx",
				DefaultExt = "xlsx",
				FileName = "source.xlsx"
			};

			if (sfd.ShowDialog() == true) {
				Exporter.SaveExport(sfd.FileName, Source);
			}
		}
	}
}
