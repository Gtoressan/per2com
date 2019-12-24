using Per2com.AdminWPF.ViewModels;
using Per2com.AdminWPF.ViewModels.BrowsedPages;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class LoginPageVMValidator : Validator<LoginPageVM>
	{
		public override LoginPageVM GetCopy(LoginPageVM item)
		{
			throw new NotImplementedException();
		}

		public override object[] GetId(LoginPageVM item)
		{
			throw new NotImplementedException();
		}

		public override bool Validate(LoginPageVM item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			else if (string.IsNullOrWhiteSpace(item.Server)) {
				MayShow(showMessage, "Ошибка", "Не указан сервер.", OK);
				return false;
			}
			else if (string.IsNullOrWhiteSpace(item.Database)) {
				MayShow(showMessage, "Ошибка", "Не указана база данных.", OK);
				return false;
			}
			else if (string.IsNullOrWhiteSpace(item.UserId)) {
				MayShow(showMessage, "Ошибка", "Не указан пользователь.", OK);
				return false;
			}
			else if (string.IsNullOrWhiteSpace(item.Password)) {
				MayShow(showMessage, "Ошибка", "Не введён пароль.", OK);
				return false;
			}

			return true;
		}
	}
}
