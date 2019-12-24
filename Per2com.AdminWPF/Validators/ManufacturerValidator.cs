using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class ManufacturerValidator : Validator<Manufacturer>
	{
		public override Manufacturer GetCopy(Manufacturer item)
		{
			return new Manufacturer(item.Id) {
				Country = item.Country,
				Name = item.Name
			};
		}

		public override object[] GetId(Manufacturer item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Manufacturer item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			else if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование производителя.", OK);
				return false;
			}
			else if (string.IsNullOrWhiteSpace(item.Country)) {
				MayShow(showMessage, "Ошибка", "Не указана страна производителя.", OK);
				return false;
			}

			return true;
		}
	}
}
