using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class RamValidator : Validator<Ram>
	{
		Directory<Manufacturer> Manufacturers => new ManufacturerDir(MainWindowVM.Default.Bridge);

		Directory<RamType> RamTypes => new RamTypeDir(MainWindowVM.Default.Bridge);

		public override Ram GetCopy(Ram item)
		{
			return new Ram(item.Id) {
				Capacity = item.Capacity,
				Frequency = item.Frequency,
				Manufacturer = item.Manufacturer,
				ManufacturerId = item.ManufacturerId,
				RamType = item.RamType,
				Name = item.Name,
				CL = item.CL,
				Manufacturers = Manufacturers.Get(nameof(GetCopy)),
				RamTypes = RamTypes.Get(nameof(GetCopy) + "RamType")
			};
		}

		public override object[] GetId(Ram item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Ram item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			if (item.Manufacturer is null) {
				MayShow(showMessage, "Ошибка", "Не выбран производитель.", OK);
				return false;
			}
			if (item.RamType is null) {
				MayShow(showMessage, "Ошибка", "Не выбран тип ОЗУ.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование ОЗУ.", OK);
				return false;
			}
			if (item.Capacity <= 0) {
				MayShow(showMessage, "Ошибка", "Не указан объем ОЗУ.", OK);
				return false;
			}
			if (item.Frequency <= 0) {
				MayShow(showMessage, "Ошибка", "Не указана частота ОЗУ.", OK);
				return false;
			}
			if (item.CL <= 0) {
				MayShow(showMessage, "Ошибка", "Не указан параметр CL ОЗУ.", OK);
				return false;
			}

			return true;
		}
	}
}
