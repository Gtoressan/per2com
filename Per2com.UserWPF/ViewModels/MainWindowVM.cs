using Per2com.AdminWPF.Filtrators;
using Per2com.AdminWPF.ResultHandlers;
using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Bridges;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using Per2com.UserWPF.ExcelExporters;
using Per2com.UserWPF.ViewModels.BrowsedPages;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Per2com.UserWPF.ViewModels
{
	public class MainWindowVM : BaseVM
	{
		Page activePage;

		public static MainWindowVM Default { get; } = new MainWindowVM();

		public Bridge Bridge { get; protected set; } = new MySqlBridge();

		public Page ActivePage {
			get => activePage;
			protected set => SetValue(ref activePage, value);
		}

		public ICommand SetPage => new CommandDelegate {
			ExecuteDelegate = (x) => GoTo((string)x, null)
		};

		MainWindowVM()
		{
			SetConnectionString();
			GoTo("Hdd/IndexPage", null);
		}

		public void SetConnectionString()
		{
			Bridge.TryConnect(
				nameof(SetConnectionString),
				"rc1a-768ipvapujcui5yo.mdb.yandexcloud.net",
				"per2Base",
				"defaultUser",
				"password",
				true
			);
		}

		public void GoTo(string path, object value)
		{
			if (path is null) {
				throw new ArgumentNullException(nameof(path));
			}

			if (ActivePage != null) {
				(ActivePage.DataContext as dynamic).Untie(Bridge);
			}

			switch (path) {
				default: throw new NotImplementedException();

				case "Hdd/IndexPage": {
					ActivePage = new IndexPageVM<Hdd>()
						.Bind(
							page: new Views.Hdd.IndexPage(),
							owner: this,
							bridge: null,
							handler: new HddHandler(),
							exporter: new HddExporter(),
							directory: new HddDir(Bridge),
							validator: null,
							filtrator: new HddFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Hdd>).SetSource();
					break;
				}

				case "Ssd/IndexPage": {
					ActivePage = new IndexPageVM<Ssd>()
						.Bind(
							page: new Views.Ssd.IndexPage(),
							owner: this,
							bridge: null,
							handler: new SsdHandler(),
							exporter: new SsdExporter(),
							directory: new SsdDir(Bridge),
							validator: null,
							filtrator: new SsdFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Ssd>).SetSource();
					break;
				}

				case "Ram/IndexPage": {
					ActivePage = new IndexPageVM<Ram>()
						.Bind(
							page: new Views.Ram.IndexPage(),
							owner: this,
							bridge: null,
							handler: new RamHandler(),
							exporter: new RamExporter(),
							directory: new RamDir(Bridge),
							validator: null,
							filtrator: new RamFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Ram>).SetSource();
					break;
				}

				case "GraphicsCard/IndexPage": {
					ActivePage = new IndexPageVM<GraphicsCard>()
						.Bind(
							page: new Views.GraphicsCard.IndexPage(),
							owner: this,
							bridge: null,
							handler: new GraphicsCardHandler(),
							exporter: new GraphicsCardExporter(),
							directory: new GraphicsCardDir(Bridge),
							validator: null,
							filtrator: new GraphicsCardFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<GraphicsCard>).SetSource();
					break;
				}

				case "Cpu/IndexPage": {
					ActivePage = new IndexPageVM<Cpu>()
						.Bind(
							page: new Views.Cpu.IndexPage(),
							owner: this,
							bridge: null,
							handler: new CpuHandler(),
							exporter: new CpuExporter(),
							directory: new CpuDir(Bridge),
							validator: null,
							filtrator: new CpuFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Cpu>).SetSource();
					break;
				}

				case "Motherboard/IndexPage": {
					ActivePage = new IndexPageVM<Motherboard>()
						.Bind(
							page: new Views.Motherboard.IndexPage(),
							owner: this,
							bridge: null,
							handler: new MotherboardHandler(),
							exporter: new MotherboardExporter(),
							directory: new MotherboardDir(Bridge),
							validator: null,
							filtrator: new MotherboardFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Motherboard>).SetSource();
					break;
				}

				case "PowerSupply/IndexPage": {
					ActivePage = new IndexPageVM<PowerSupply>()
						.Bind(
							page: new Views.PowerSupply.IndexPage(),
							owner: this,
							bridge: null,
							handler: new PowerSupplyHandler(),
							exporter: new PowerSupplyExporter(),
							directory: new PowerSupplyDir(Bridge),
							validator: null,
							filtrator: new PowerSupplyFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<PowerSupply>).SetSource();
					break;
				}
			}
		}
	}
}
