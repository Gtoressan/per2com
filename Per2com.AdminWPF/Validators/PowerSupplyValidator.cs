using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class PowerSupplyValidator : Validator<PowerSupply>
	{
		Directory<Manufacturer> Manufacturers => new ManufacturerDir(MainWindowVM.Default.Bridge);

		public override PowerSupply GetCopy(PowerSupply item)
		{
			return new PowerSupply {
				Id = item.Id,
				FormFactor = item.FormFactor,
				Manufacturer = item.Manufacturer,
				Name = item.Name,
				Power = item.Power,
				Manufacturers = Manufacturers.Get(nameof(GetCopy))
			};
		}

		public override object[] GetId(PowerSupply item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(PowerSupply item, bool showMessage)
		{
			if (item is null) {
				throw new System.ArgumentNullException(nameof(item));
			}
			if (item.Manufacturer is null) {
				MayShow(showMessage, "Ошибка", "Не выбран производитель.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование блока питания.", OK);
				return false;
			}
			if (item.Power <= 0) {
				MayShow(showMessage, "Ошибка", "Не указана мощность блока питания.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.FormFactor)) {
				MayShow(showMessage, "Ошибка", "Не указан форм-фактор блока питания.", OK);
				return false;
			}

			return true;
		}
	}
}
