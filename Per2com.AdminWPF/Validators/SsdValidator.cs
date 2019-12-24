using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class SsdValidator : Validator<Ssd>
	{
		Directory<Manufacturer> Manufacturers => new ManufacturerDir(MainWindowVM.Default.Bridge);

		public override Ssd GetCopy(Ssd item)
		{
			return new Ssd(item.Id) {
				Capacity = item.Capacity,
				FormFactor = item.FormFactor,
				Manufacturer = item.Manufacturer,
				ManufacturerId = item.ManufacturerId,
				Name = item.Name,
				Manufacturers = Manufacturers.Get(nameof(GetCopy))
			};
		}

		public override object[] GetId(Ssd item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Ssd item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			if (item.Manufacturer is null) {
				MayShow(showMessage, "Ошибка", "Не выбран производитель.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование SSD.", OK);
				return false;
			}
			if (item.Capacity <= 0) {
				MayShow(showMessage, "Ошибка", "Не указан объем SSD.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.FormFactor)) {
				MayShow(showMessage, "Ошибка", "Не указан форм-фактор SSD.", OK);
				return false;
			}

			return true;
		}
	}
}
