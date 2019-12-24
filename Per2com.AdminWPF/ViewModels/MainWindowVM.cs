using Per2com.AdminWPF.Filtrators;
using Per2com.AdminWPF.ResultHandlers;
using Per2com.AdminWPF.Validators;
using Per2com.AdminWPF.ViewModels.BrowsedPages;
using Per2com.AdminWPF.Views;
using Per2com.DataModel;
using Per2com.DataModel.Bridges;
using Per2com.DataModel.Descriptors;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Per2com.AdminWPF.ViewModels
{
	public class MainWindowVM : BaseVM
	{
		Page activePage;
		bool isMenuEnabled;

		public static MainWindowVM Default { get; } = new MainWindowVM();

		public Bridge Bridge { get; } = new MySqlBridge();

		public Page ActivePage {
			get => activePage;
			protected set => SetValue(ref activePage, value);
		}

		public bool IsMenuEnabled {
			get => isMenuEnabled;
			protected set => SetValue(ref isMenuEnabled, value);
		}

		public ICommand SetPage => new CommandDelegate {
			ExecuteDelegate = (x) => GoTo((string)x, null)
		};

		MainWindowVM() => GoTo("LoginPage", null);

		public void GoTo(string path, object value)
		{
			if (path is null) {
				throw new ArgumentNullException(nameof(path));
			}

			if (ActivePage != null && ActivePage.GetType() != typeof(DataGripPage)) {
				(ActivePage.DataContext as dynamic).Untie(Bridge);
			}

			switch (path) {
				default: throw new NotImplementedException();

				case "LoginPage": {
					IsMenuEnabled = false;
					ActivePage = new LoginPageVM()
						.Bind(
							page: new LoginPage(),
							owner: this,
							bridge: Bridge,
							handler: new LoginPageVMHandler(),
							directory: null,
							validator: new LoginPageVMValidator(),
							filtrator: null
						);
					break;
				}

				case "QueryBuilder": {
					IsMenuEnabled = true;
					ActivePage = new QueryBuilderPageVM()
						.Bind(
							page: new QueryBuilderPage(),
							owner: this,
							handler: new QueryBuilderHandler(),
							validator: new QueryBuilderPageVMValidator(),
							descriptor: new MySqlDescriptor(Bridge)
						);
					(ActivePage.DataContext as QueryBuilderPageVM).SetTables();
					break;
				}

				case "DataGripPage": {
					IsMenuEnabled = true;
					var a = value as Tuple<object[][], DescriptionToken[]>;
					ActivePage = new DataGripPage(a.Item1 as object[][], a.Item2, this);
					break;
				}

				case "Manufacturer/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Manufacturer>()
						.Bind(
							page: new Views.Manufacturer.CreatePage(),
							owner: this,
							bridge: null,
							handler: new ManufacturerHandler(),
							directory: new ManufacturerDir(Bridge),
							validator: new ManufacturerValidator(),
							filtrator: new ManufacturerFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Manufacturer>).SetItem();
					break;
				}

				case "Manufacturer/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Manufacturer>((Manufacturer)value)
						.Bind(
							page: new Views.Manufacturer.EditPage(),
							owner: this,
							bridge: null,
							handler: new ManufacturerHandler(),
							directory: new ManufacturerDir(Bridge),
							validator: new ManufacturerValidator(),
							filtrator: new ManufacturerFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Manufacturer>).SetItem();
					break;
				}

				case "Manufacturer/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Manufacturer>()
						.Bind(
							page: new Views.Manufacturer.IndexPage(),
							owner: this,
							bridge: null,
							handler: new ManufacturerHandler(),
							directory: new ManufacturerDir(Bridge),
							validator: new ManufacturerValidator(),
							filtrator: new ManufacturerFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Manufacturer>).SetSource();
					break;
				}

				case "Hdd/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Hdd>()
						.Bind(
							page: new Views.Hdd.CreatePage(),
							owner: this,
							bridge: null,
							handler: new HddHandler(),
							directory: new HddDir(Bridge),
							validator: new HddValidator(),
							filtrator: new HddFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Hdd>).SetItem();
					break;
				}

				case "Hdd/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Hdd>((Hdd)value)
						.Bind(
							page: new Views.Hdd.EditPage(),
							owner: this,
							bridge: null,
							handler: new HddHandler(),
							directory: new HddDir(Bridge),
							validator: new HddValidator(),
							filtrator: new HddFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Hdd>).SetItem();
					break;
				}

				case "Hdd/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Hdd>()
						.Bind(
							page: new Views.Hdd.IndexPage(),
							owner: this,
							bridge: null,
							handler: new HddHandler(),
							directory: new HddDir(Bridge),
							validator: new HddValidator(),
							filtrator: new HddFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Hdd>).SetSource();
					break;
				}

				case "Ssd/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Ssd>()
						.Bind(
							page: new Views.Ssd.CreatePage(),
							owner: this,
							bridge: null,
							handler: new SsdHandler(),
							directory: new SsdDir(Bridge),
							validator: new SsdValidator(),
							filtrator: new SsdFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Ssd>).SetItem();
					break;
				}

				case "Ssd/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Ssd>((Ssd)value)
						.Bind(
							page: new Views.Ssd.EditPage(),
							owner: this,
							bridge: null,
							handler: new SsdHandler(),
							directory: new SsdDir(Bridge),
							validator: new SsdValidator(),
							filtrator: new SsdFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Ssd>).SetItem();
					break;
				}

				case "Ssd/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Ssd>()
						.Bind(
							page: new Views.Ssd.IndexPage(),
							owner: this,
							bridge: null,
							handler: new SsdHandler(),
							directory: new SsdDir(Bridge),
							validator: new SsdValidator(),
							filtrator: new SsdFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Ssd>).SetSource();
					break;
				}

				case "RamType/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<RamType>()
						.Bind(
							page: new Views.RamType.CreatePage(),
							owner: this,
							bridge: null,
							handler: new RamTypeHandler(),
							directory: new RamTypeDir(Bridge),
							validator: new RamTypeValidator(),
							filtrator: new RamTypeFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<RamType>).SetItem();
					break;
				}

				case "RamType/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<RamType>((RamType)value)
						.Bind(
							page: new Views.RamType.EditPage(),
							owner: this,
							bridge: null,
							handler: new RamTypeHandler(),
							directory: new RamTypeDir(Bridge),
							validator: new RamTypeValidator(),
							filtrator: new RamTypeFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<RamType>).SetItem();
					break;
				}

				case "RamType/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<RamType>()
						.Bind(
							page: new Views.RamType.IndexPage(),
							owner: this,
							bridge: null,
							handler: new RamTypeHandler(),
							directory: new RamTypeDir(Bridge),
							validator: new RamTypeValidator(),
							filtrator: new RamTypeFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<RamType>).SetSource();
					break;
				}

				case "Ram/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Ram>()
						.Bind(
							page: new Views.Ram.CreatePage(),
							owner: this,
							bridge: null,
							handler: new RamHandler(),
							directory: new RamDir(Bridge),
							validator: new RamValidator(),
							filtrator: new RamFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Ram>).SetItem();
					break;
				}

				case "Ram/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Ram>((Ram)value)
						.Bind(
							page: new Views.Ram.EditPage(),
							owner: this,
							bridge: null,
							handler: new RamHandler(),
							directory: new RamDir(Bridge),
							validator: new RamValidator(),
							filtrator: new RamFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Ram>).SetItem();
					break;
				}

				case "Ram/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Ram>()
						.Bind(
							page: new Views.Ram.IndexPage(),
							owner: this,
							bridge: null,
							handler: new RamHandler(),
							directory: new RamDir(Bridge),
							validator: new RamValidator(),
							filtrator: new RamFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Ram>).SetSource();
					break;
				}

				case "GraphicsCard/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<GraphicsCard>()
						.Bind(
							page: new Views.GraphicsCard.CreatePage(),
							owner: this,
							bridge: null,
							handler: new GraphicsCardHandler(),
							directory: new GraphicsCardDir(Bridge),
							validator: new GraphicsCardValidator(),
							filtrator: new GraphicsCardFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<GraphicsCard>).SetItem();
					break;
				}

				case "GraphicsCard/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<GraphicsCard>((GraphicsCard)value)
						.Bind(
							page: new Views.GraphicsCard.EditPage(),
							owner: this,
							bridge: null,
							handler: new GraphicsCardHandler(),
							directory: new GraphicsCardDir(Bridge),
							validator: new GraphicsCardValidator(),
							filtrator: new GraphicsCardFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<GraphicsCard>).SetItem();
					break;
				}

				case "GraphicsCard/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<GraphicsCard>()
						.Bind(
							page: new Views.GraphicsCard.IndexPage(),
							owner: this,
							bridge: null,
							handler: new GraphicsCardHandler(),
							directory: new GraphicsCardDir(Bridge),
							validator: new GraphicsCardValidator(),
							filtrator: new GraphicsCardFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<GraphicsCard>).SetSource();
					break;
				}

				case "Socket/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Socket>()
						.Bind(
							page: new Views.Socket.CreatePage(),
							owner: this,
							bridge: null,
							handler: new SocketHandler(),
							directory: new SocketDir(Bridge),
							validator: new SocketValidator(),
							filtrator: new SocketFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Socket>).SetItem();
					break;
				}

				case "Socket/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Socket>((Socket)value)
						.Bind(
							page: new Views.Socket.EditPage(),
							owner: this,
							bridge: null,
							handler: new SocketHandler(),
							directory: new SocketDir(Bridge),
							validator: new SocketValidator(),
							filtrator: new SocketFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Socket>).SetItem();
					break;
				}

				case "Socket/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Socket>()
						.Bind(
							page: new Views.Socket.IndexPage(),
							owner: this,
							bridge: null,
							handler: new SocketHandler(),
							directory: new SocketDir(Bridge),
							validator: new SocketValidator(),
							filtrator: new SocketFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Socket>).SetSource();
					break;
				}

				case "Cpu/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Cpu>()
						.Bind(
							page: new Views.Cpu.CreatePage(),
							owner: this,
							bridge: null,
							handler: new CpuHandler(),
							directory: new CpuDir(Bridge),
							validator: new CpuValidator(),
							filtrator: new CpuFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Cpu>).SetItem();
					break;
				}

				case "Cpu/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Cpu>((Cpu)value)
						.Bind(
							page: new Views.Cpu.EditPage(),
							owner: this,
							bridge: null,
							handler: new CpuHandler(),
							directory: new CpuDir(Bridge),
							validator: new CpuValidator(),
							filtrator: new CpuFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Cpu>).SetItem();
					break;
				}

				case "Cpu/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Cpu>()
						.Bind(
							page: new Views.Cpu.IndexPage(),
							owner: this,
							bridge: null,
							handler: new CpuHandler(),
							directory: new CpuDir(Bridge),
							validator: new CpuValidator(),
							filtrator: new CpuFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Cpu>).SetSource();
					break;
				}

				case "Motherboard/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<Motherboard>()
						.Bind(
							page: new Views.Motherboard.CreatePage(),
							owner: this,
							bridge: null,
							handler: new MotherboardHandler(),
							directory: new MotherboardDir(Bridge),
							validator: new MotherboardValidator(),
							filtrator: new MotherboardFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<Motherboard>).SetItem();
					break;
				}

				case "Motherboard/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<Motherboard>((Motherboard)value)
						.Bind(
							page: new Views.Motherboard.EditPage(),
							owner: this,
							bridge: null,
							handler: new MotherboardHandler(),
							directory: new MotherboardDir(Bridge),
							validator: new MotherboardValidator(),
							filtrator: new MotherboardFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<Motherboard>).SetItem();
					break;
				}

				case "Motherboard/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<Motherboard>()
						.Bind(
							page: new Views.Motherboard.IndexPage(),
							owner: this,
							bridge: null,
							handler: new MotherboardHandler(),
							directory: new MotherboardDir(Bridge),
							validator: new MotherboardValidator(),
							filtrator: new MotherboardFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<Motherboard>).SetSource();
					break;
				}

				case "PowerSupply/CreatePage": {
					IsMenuEnabled = true;
					ActivePage = new CreatePageVM<PowerSupply>()
						.Bind(
							page: new Views.PowerSupply.CreatePage(),
							owner: this,
							bridge: null,
							handler: new PowerSupplyHandler(),
							directory: new PowerSupplyDir(Bridge),
							validator: new PowerSupplyValidator(),
							filtrator: new PowerSupplyFiltrator()
						);
					(ActivePage.DataContext as CreatePageVM<PowerSupply>).SetItem();
					break;
				}

				case "PowerSupply/EditPage": {
					IsMenuEnabled = true;
					ActivePage = new EditPageVM<PowerSupply>((PowerSupply)value)
						.Bind(
							page: new Views.PowerSupply.EditPage(),
							owner: this,
							bridge: null,
							handler: new PowerSupplyHandler(),
							directory: new PowerSupplyDir(Bridge),
							validator: new PowerSupplyValidator(),
							filtrator: new PowerSupplyFiltrator()
						);
					(ActivePage.DataContext as EditPageVM<PowerSupply>).SetItem();
					break;
				}

				case "PowerSupply/IndexPage": {
					IsMenuEnabled = true;
					ActivePage = new IndexPageVM<PowerSupply>()
						.Bind(
							page: new Views.PowerSupply.IndexPage(),
							owner: this,
							bridge: null,
							handler: new PowerSupplyHandler(),
							directory: new PowerSupplyDir(Bridge),
							validator: new PowerSupplyValidator(),
							filtrator: new PowerSupplyFiltrator()
						);
					(ActivePage.DataContext as IndexPageVM<PowerSupply>).SetSource();
					break;
				}
			}
		}
	}
}
