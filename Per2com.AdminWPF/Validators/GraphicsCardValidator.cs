using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using Per2com.DataModel.Directories;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class GraphicsCardValidator : Validator<GraphicsCard>
	{
		Directory<Manufacturer> Manufacturers = new ManufacturerDir(MainWindowVM.Default.Bridge);

		public override GraphicsCard GetCopy(GraphicsCard item)
		{
			return new GraphicsCard(item.Id) {
				ManufacturerId = item.ManufacturerId,
				Name = item.Name,
				Capacity = item.Capacity,
				MemoryType = item.MemoryType,
				Manufacturers = Manufacturers.Get(nameof(GetCopy))
			};
		}

		public override object[] GetId(GraphicsCard item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(GraphicsCard item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			if (item.Manufacturer is null) {
				MayShow(showMessage, "Ошибка", "Не выбран производитель.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование видеокарты.", OK);
				return false;
			}
			if (item.Capacity <= 0) {
				MayShow(showMessage, "Ошибка", "Не указан объем памяти видеокарты.", OK);
				return false;
			}
			if (string.IsNullOrWhiteSpace(item.MemoryType)) {
				MayShow(showMessage, "Ошибка", "Не указано тип графической памяти.", OK);
				return false;
			}

			return true;
		}
	}
}
