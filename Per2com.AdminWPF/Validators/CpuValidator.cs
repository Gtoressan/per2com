using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class CpuValidator : Validator<Cpu>
	{
		Directory<Manufacturer> Manufacturers = new ManufacturerDir(MainWindowVM.Default.Bridge);

		Directory<Socket> Sockets = new SocketDir(MainWindowVM.Default.Bridge);

		public override Cpu GetCopy(Cpu item)
		{
			return new Cpu {
				CoreCount = item.CoreCount,
				Frequency = item.Frequency,
				Id = item.Id,
				L1 = item.L1,
				L2 = item.L2,
				L3 = item.L3,
				Manufacturer = item.Manufacturer,
				Manufacturers = Manufacturers.Get(nameof(GetCopy)),
				Name = item.Name,
				Socket = item.Socket,
				Sockets = Sockets.Get(nameof(GetCopy) + "Socket"),
				ThreadCount = item.ThreadCount
			};
		}

		public override object[] GetId(Cpu item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Cpu item, bool showMessage)
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
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование процессора.", OK);
				return false;
			}
			if (item.ThreadCount <= 0) {
				MayShow(showMessage, "Ошибка", "Не указано число потоков.", OK);
				return false;
			}
			if (item.CoreCount <= 0) {
				MayShow(showMessage, "Ошибка", "Не указано число ядер.", OK);
				return false;
			}
			if (item.Frequency <= 0) {
				MayShow(showMessage, "Ошибка", "Не указана частота процессора.", OK);
				return false;
			}
			if (item.L1 < 0 || item.L2 < 0 || item.L3 < 0) {
				MayShow(showMessage, "Ошибка", "Объем кеш-памяти процессора не может быть меньше 0.", OK);
				return false;
			}

			return true;
		}
	}
}
