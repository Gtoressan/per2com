using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel.Entities;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class SocketValidator : Validator<Socket>
	{
		public override Socket GetCopy(Socket item)
		{
			return new Socket {
				Id = item.Id,
				Name = item.Name
			};
		}

		public override object[] GetId(Socket item)
		{
			return new object[] { item.Id };
		}

		public override bool Validate(Socket item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			else if (string.IsNullOrWhiteSpace(item.Name)) {
				MayShow(showMessage, "Ошибка", "Не указано наименование сокета.", OK);
				return false;
			}

			return true;
		}
	}
}
