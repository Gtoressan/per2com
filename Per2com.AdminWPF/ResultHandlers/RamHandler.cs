using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using System;
using System.Collections.Generic;
using System.Windows;

using create = Per2com.AdminWPF.ViewModels.BrowsedPages.CreatePageVM<Per2com.DataModel.Entities.Ram>;
using edit = Per2com.AdminWPF.ViewModels.BrowsedPages.EditPageVM<Per2com.DataModel.Entities.Ram>;
using index = Per2com.AdminWPF.ViewModels.BrowsedPages.IndexPageVM<Per2com.DataModel.Entities.Ram>;
using validator = Per2com.AdminWPF.ViewModels.Validator<Per2com.DataModel.Entities.Ram>;

using static System.Windows.MessageBox;
using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.ResultHandlers
{
	public class RamHandler : ResultHandler
	{
		public override Dictionary<string, string> Paths => new Dictionary<string, string> {
			["createPage"] = "Ram/CreatePage",
			["editPage"] = "Ram/EditPage",
			["indexPage"] = "Ram/IndexPage",
		};

		public override void Handler(Bridge bridge, BridgeEventArgs args)
		{
			switch (args.Tag) {
				default: throw new NotImplementedException();

				case nameof(create.AddItem) when !args.IsSuccessful: {
					Show(
						$"При добавлении ОЗУ \"{((create)DataContext).Item}\" произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(create.AddItem) when args.IsSuccessful: {
					if (Show(
							$"ОЗУ \"{((create)DataContext).Item}\" успешно внесено в базу.\nОткрыть список ОЗУ?",
							"Сообщение",
							YesNo
						) == MessageBoxResult.Yes) {
						MainWindowVM.Default.GoTo(Paths["indexPage"], null);
					}
					break;
				}

				case nameof(edit.EditItem) when !args.IsSuccessful: {
					Show(
						$"При изменении \"{((edit)DataContext).OldItem}\" на \"{((edit)DataContext).Item}\" произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(edit.EditItem) when args.IsSuccessful: {
					if (Show(
							$"ОЗУ \"{((edit)DataContext).OldItem}\" успешно изменено на \"{((edit)DataContext).Item}\".\nОткрыть список ОЗУ?",
							"Сообщение",
							YesNo
						) == MessageBoxResult.Yes) {
						MainWindowVM.Default.GoTo(Paths["indexPage"], null);
					}
					break;
				}

				case nameof(index.DropItem) when !args.IsSuccessful: {
					Show(
						$"При удалении \"{((index)DataContext).SelectedItem}\" произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(index.DropItem) when args.IsSuccessful: {
					MainWindowVM.Default.GoTo(Paths["indexPage"], null);
					break;
				}

				case nameof(index.SetSource) when args.IsSuccessful: break;

				case nameof(index.SetSource) when !args.IsSuccessful: {
					Show(
						$"При загрузке списка ОЗУ произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(index.FilterSource) when args.IsSuccessful: break;

				case nameof(index.FilterSource) when !args.IsSuccessful: {
					Show(
						$"При загрузке списка ОЗУ для выборки произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(validator.GetCopy) when args.IsSuccessful: break;

				case nameof(validator.GetCopy) when !args.IsSuccessful: {
					Show(
						$"При загрузке списка производителей произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(validator.GetCopy) + "RamType" when args.IsSuccessful: break;

				case nameof(validator.GetCopy) + "RamType" when !args.IsSuccessful: {
					Show(
						$"При загрузке списка типов ОЗУ произошла ошибка.",
						"Ошибка"
					);
					break;
				}
			}
		}
	}
}
