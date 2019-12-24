using Microsoft.Win32;
using OfficeOpenXml;
using Per2com.DataModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using static System.Environment;
using static System.Environment.SpecialFolder;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class QueryBuilderPageVM : BrowsedPageVM<QueryBuilderPageVM>
	{
		string[] tables;

		DescriptionToken[] tokens;

		public Descriptor Descriptor { get; set; }

		public string[] Tables {
			get => tables;
			protected set => SetValue(ref tables, value);
		}

		public string Table { get; set; }

		public DescriptionToken[] Tokens {
			get => tokens;
			protected set => SetValue(ref tokens, value);
		}

		public ObservableCollection<DescriptionTokenVM> TokenVM { get; } = new ObservableCollection<DescriptionTokenVM>();

		public ICommand GetTokens => new CommandDelegate {
			ExecuteDelegate = (x) => SetTokens()
		};

		public ICommand AddToken => new CommandDelegate {
			ExecuteDelegate = (x) => AddTokenVM()
		};

		public ICommand RemoveToken => new CommandDelegate {
			ExecuteDelegate = (x) => RemoveTokenVM((int)x)
		};

		public ICommand Select => new CommandDelegate {
			ExecuteDelegate = (x) => GetData()
		};

		public QueryBuilderPageVM() { }

		public void SetTables()
		{
			Tables = Descriptor.GetTables(nameof(SetTables));
		}

		public void SetTokens()
		{
			if (Table != null) {
				Tokens = Descriptor.GetTokens(nameof(SetTokens), Table);
				TokenVM.Clear();
			} else {
				MessageBox.Show("Не выбрана целевая таблица.", "Ошибка");
			}
		}

		public void AddTokenVM()
		{
			if (Table != null) {
				TokenVM.Add(new DescriptionTokenVM {
					Id = TokenVM.Count,
					Token = new DescriptionToken(),
					Tokens = Tokens?.Select(i => (DescriptionToken)i.Clone()).ToArray()
				});
			}
		}

		public void RemoveTokenVM(int id)
		{
			TokenVM.Remove(TokenVM.First(x => x.Id == id));
		}

		public void GetData()
		{
			if (Validator.Validate(this, true)) {
				var source = Descriptor.Select(nameof(GetData), Table, TokenVM.Select(x => x.Token).ToArray());

				var sfd = new SaveFileDialog() {
					InitialDirectory = GetFolderPath(Desktop),
					CheckPathExists = true,
					Title = "Экспорт данных",
					Filter = "Excel Files (*.xlsx) | *.xlsx",
					DefaultExt = "xlsx",
					FileName = "generated table.xlsx"
				};

				if (sfd.ShowDialog() == true) {
					Save(source, sfd.FileName);
				}
			}
		}

		public Page Bind(Page page, BaseVM owner, ResultHandler handler, Validator<QueryBuilderPageVM> validator, Descriptor descriptor)
		{
			Owner = owner;
			Handler = handler;
			Validator = validator;
			Descriptor = descriptor;

			if (page != null) {
				page.DataContext = this;
			}

			if (Handler != null) {
				Handler.DataContext = this;
			}

			if (Descriptor != null && Handler != null) {
				Descriptor.Bridge.GotResut += Handler.Handler;
			}

			return page;
		}

		public override void Untie(Bridge bridge)
		{
			if (Descriptor != null && Handler != null) {
				Descriptor.Bridge.GotResut -= Handler.Handler;
			}

			if (Handler != null) {
				Handler.DataContext = null;
			}

			Descriptor = null;
			Validator = null;
			Handler = null;
			Owner = null;
		}

		public void Save(object[][] source, string path)
		{
			var package = new ExcelPackage();
			var sheet = package.Workbook.Worksheets.Add("Сгенерированная таблица");

			var headers = TokenVM.Select(x => x.Token).ToArray();
			for (int i = 0; i < headers.Length; ++i) {
				sheet.Cells[$"{(char)(i + 65)}1"].Value = headers[i].ToString();
			}

			for (int i = 0; i < source.Length; ++i) {
				for (int j = 0; j < source[i].Length; ++j) {
					sheet.Cells[$"{(char)(j + 65)}{i + 2}"].Value = source[i][j];
				}
			}

			try {
				package.SaveAs(new FileInfo(path));
			}
			catch {
				MessageBox.Show("При сохранении файла возникла ошибка.", "ошибка");
			}
		}
	}
}
