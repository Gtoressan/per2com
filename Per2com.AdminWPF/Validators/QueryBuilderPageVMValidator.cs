using Per2com.AdminWPF.ViewModels;
using Per2com.AdminWPF.ViewModels.BrowsedPages;
using System;

using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.Validators
{
	public class QueryBuilderPageVMValidator : Validator<QueryBuilderPageVM>
	{
		public override QueryBuilderPageVM GetCopy(QueryBuilderPageVM item)
		{
			throw new NotImplementedException();
		}

		public override object[] GetId(QueryBuilderPageVM item)
		{
			throw new NotImplementedException();
		}

		public override bool Validate(QueryBuilderPageVM item, bool showMessage)
		{
			if (item is null) {
				throw new ArgumentNullException(nameof(item));
			}
			else if (string.IsNullOrWhiteSpace(item.Table)) {
				MayShow(showMessage, "Ошибка", "Не выбрана целевая таблица.", OK);
				return false;
			}
			else if (item.TokenVM.Count == 0) {
				MayShow(showMessage, "Ошибка", "Не создано не одного токена.", OK);
				return false;
			}

			foreach (var i in item.TokenVM) {
				if (i.Token is null || string.IsNullOrEmpty(i.Token.Name)) {
					MayShow(showMessage, "Ошибка", $"Создан пустой токен.", OK);
					return false;
				}
				else if (i.Token.Condition is null) {
					MayShow(showMessage, "Ошибка", $"Не выбрано условие для атрибута {i.Token.Name}", OK);
					return false;
				}
				else if (i.Token.Value is null && i.Token.Condition != "без условий") {
					MayShow(showMessage, "Ошибка", $"Не указано значение для атрибута {i.Token.Name}", OK);
					return false;
				}
				else if (i.Token.Condition != "без условий" && i.Token.Type == DataModel.DescriptionTokenType.Float && !float.TryParse(i.Token.Value.ToString(), out _)) {
					MayShow(showMessage, "Ошибка", $"Значения для атрибута {i.Token.Name} должно быть числом с плавающей точкой.", OK);
					return false;
				}
				else if (i.Token.Condition != "без условий" && i.Token.Type == DataModel.DescriptionTokenType.Int && !int.TryParse(i.Token.Value.ToString(), out _)) {
					MayShow(showMessage, "Ошибка", $"Значения для атрибута {i.Token.Name} должно быть целым числом.", OK);
					return false;
				}
			}

			return true;
		}
	}
}
