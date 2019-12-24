using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class RamTypeValidator : Validator<RamType>
	{
		public override RamType GetCopy(RamType item)
		{
			return new RamType(item.Id) {
				Name = item.Name
			};
		}

		public override object[] GetId(RamType item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(RamType item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			else if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование типа ОЗУ.", OK);
				return false;
			}

			return true;
		}
	}
}
