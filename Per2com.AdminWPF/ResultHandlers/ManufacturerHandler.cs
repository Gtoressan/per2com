using Per2com.AdminWPF.ViewModels;
using Per2com.DataModel;
using System;
using System.Collections.Generic;
using System.Windows;

using create = Per2com.AdminWPF.ViewModels.BrowsedPages.CreatePageVM<Per2com.DataModel.Entities.Manufacturer>;
using edit = Per2com.AdminWPF.ViewModels.BrowsedPages.EditPageVM<Per2com.DataModel.Entities.Manufacturer>;
using index = Per2com.AdminWPF.ViewModels.BrowsedPages.IndexPageVM<Per2com.DataModel.Entities.Manufacturer>;

using static System.Windows.MessageBox;
using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.ResultHandlers
{
	public class ManufacturerHandler : ResultHandler
	{
		public override Dictionary<string, string> Paths => new Dictionary<string, string> {
			["createPage"] = "Manufacturer/CreatePage",
			["editPage"] = "Manufacturer/EditPage",
			["indexPage"] = "Manufacturer/IndexPage"
		};

		public override void Handler(Bridge bridge, BridgeEventArgs args)
		{
			switch (args.Tag) {
				default: throw new NotImplementedException();

				case nameof(create.AddItem) when !args.IsSuccessful: {
					Show(
						$"При добавлении производителя \"{((create)DataContext).Item}\" произошла.",
						"Ошибка"
					);
					break;
				}

				case nameof(create.AddItem) when args.IsSuccessful: {
					if (Show(
							$"Производитель \"{((create)DataContext).Item}\" успешно внесен в базу.\nОткрыть список производителей?",
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
							$"Производитель \"{((edit)DataContext).OldItem}\" успешно изменен на \"{((edit)DataContext).Item}\".\nОткрыть список производителей?",
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
						$"При загрузке списка производителей произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(index.FilterSource) when args.IsSuccessful: break;

				case nameof(index.FilterSource) when !args.IsSuccessful: {
					Show(
						$"При загрузке списка производителей для выборки произошла ошибка.",
						"Ошибка"
					);
					break;
				}
			}
		}
	}
}
