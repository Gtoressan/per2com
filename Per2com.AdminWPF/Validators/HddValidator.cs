using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class HddValidator : Validator<Hdd>
	{
		Directory<Manufacturer> Manufacturers => new ManufacturerDir(MainWindowVM.Default.Bridge);

		public override Hdd GetCopy(Hdd item)
		{
			return new Hdd(item.Id) {
				Capacity = item.Capacity,
				FormFactor = item.FormFactor,
				Manufacturer = item.Manufacturer,
				ManufacturerId = item.ManufacturerId,
				Name = item.Name,
				RotatingPerMinute = item.RotatingPerMinute,
				Manufacturers = Manufacturers.Get(nameof(GetCopy))
			};
		}

		public override object[] GetId(Hdd item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Hdd item, bool showMessage)
		{
			if (item is null) {
				throw new System.ArgumentNullException(nameof(item));
			}
			if (item.Manufacturer is null) {
				MayShow(showMessage, "Ошибка", "Не выбран производитель.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование HDD.", OK);
				return false;
			}
			if (item.Capacity <= 0) {
				MayShow(showMessage, "Ошибка", "Не указан объем HDD.", OK);
				return false;
			}
			if (item.RotatingPerMinute <= 0) {
				MayShow(showMessage, "Ошибка", "Не указана скорость вращения диска.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.FormFactor)) {
				MayShow(showMessage, "Ошибка", "Не указан форм-фактор HDD.", OK);
				return false;
			}

			return true;
		}
	}
}
