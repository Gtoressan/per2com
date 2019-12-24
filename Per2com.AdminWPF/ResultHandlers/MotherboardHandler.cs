using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using System;
using System.Collections.Generic;
using System.Windows;

using create = Per2com.AdminWPF.ViewModels.BrowsedPages.CreatePageVM<Per2com.DataModel.Entities.Motherboard>;
using edit = Per2com.AdminWPF.ViewModels.BrowsedPages.EditPageVM<Per2com.DataModel.Entities.Motherboard>;
using index = Per2com.AdminWPF.ViewModels.BrowsedPages.IndexPageVM<Per2com.DataModel.Entities.Motherboard>;
using validator = Per2com.AdminWPF.ViewModels.Validator<Per2com.DataModel.Entities.Motherboard>;

using static System.Windows.MessageBox;
using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.ResultHandlers
{
	public class MotherboardHandler : ResultHandler
	{
		public override Dictionary<string, string> Paths => new Dictionary<string, string> {
			["createPage"] = "Motherboard/CreatePage",
			["editPage"] = "Motherboard/EditPage",
			["indexPage"] = "Motherboard/IndexPage",
		};

		public override void Handler(Bridge bridge, BridgeEventArgs args)
		{
			switch (args.Tag) {
				default: throw new NotImplementedException();

				case nameof(create.AddItem) when !args.IsSuccessful: {
					Show(
						$"При добавлении материнской платы \"{((create)DataContext).Item}\" произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(create.AddItem) when args.IsSuccessful: {
					if (Show(
							$"Мат. плата \"{((create)DataContext).Item}\" успешно внесен в базу.\nОткрыть список мат. плат?",
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
							$"Мат. плата \"{((edit)DataContext).OldItem}\" успешно изменена на \"{((edit)DataContext).Item}\".\nОткрыть список мат. плат?",
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
						$"При загрузке списка материнских плат произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(index.FilterSource) when args.IsSuccessful: break;

				case nameof(index.FilterSource) when !args.IsSuccessful: {
					Show(
						$"При загрузке списка мат. плат для выборки произошла ошибка.",
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

				case nameof(validator.GetCopy) + "Socket" when args.IsSuccessful: break;

				case nameof(validator.GetCopy) + "Socket" when !args.IsSuccessful: {
					Show(
						$"При загрузке списка сокетов произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(validator.GetCopy) + "RamType" when args.IsSuccessful: break;

				case nameof(validator.GetCopy) + "RamType" when !args.IsSuccessful: {
					Show(
						$"При загрузке списка типа ОЗУ произошла ошибка.",
						"Ошибка"
					);
					break;
				}
			}
		}
	}
}
