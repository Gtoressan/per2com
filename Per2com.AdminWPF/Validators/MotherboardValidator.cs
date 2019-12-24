using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class MotherboardValidator : Validator<Motherboard>
	{
		Directory<Manufacturer> Manufacturers = new ManufacturerDir(MainWindowVM.Default.Bridge);

		Directory<Socket> Sockets = new SocketDir(MainWindowVM.Default.Bridge);

		Directory<RamType> RamTypes = new RamTypeDir(MainWindowVM.Default.Bridge);

		public override Motherboard GetCopy(Motherboard item)
		{
			return new Motherboard {
				Id = item.Id,
				Manufacturer = item.Manufacturer,
				Manufacturers = Manufacturers.Get(nameof(GetCopy)),
				Name = item.Name,
				RamCount = item.RamCount,
				RamType = item.RamType,
				RamTypes = RamTypes.Get(nameof(GetCopy) + "RamType"),
				Socket = item.Socket,
				Sockets = Sockets.Get(nameof(GetCopy) + "Socket")
			};
		}

		public override object[] GetId(Motherboard item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Motherboard item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			if (item.Manufacturer is null) {
				MayShow(showMessage, "Ошибка", "Не выбран производитель.", OK);
				return false;
			}
			if (item.Socket is null) {
				MayShow(showMessage, "Ошибка", "Не выбран сокет.", OK);
				return false;
			}
			if (item.RamType is null) {
				MayShow(showMessage, "Ошибка", "Не выбран тип поддерживаемой оперативной памяти.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование материнской платы.", OK);
				return false;
			}
			if (item.RamCount <= 0) {
				MayShow(showMessage, "Ошибка", "Не указано число плашек оперативной памяти.", OK);
				return false;
			}

			return true;
		}
	}
}
