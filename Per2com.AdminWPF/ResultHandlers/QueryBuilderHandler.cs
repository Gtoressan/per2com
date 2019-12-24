using System;
using System.Collections.Generic;
using Per2com.AdminWPF.ViewModels;
using Per2com.AdminWPF.ViewModels.BrowsedPages;
using Per2com.DataModel;

using static System.Windows.MessageBox;
using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.ResultHandlers
{
	public class QueryBuilderHandler : ResultHandler
	{
		public override Dictionary<string, string> Paths => new Dictionary<string, string> {
			[""] = ""
		};

		public override void Handler(Bridge bridge, BridgeEventArgs args)
		{
			switch (args.Tag) {
				default: throw new NotImplementedException();

				case nameof(QueryBuilderPageVM.SetTables) when args.IsSuccessful: break;

				case nameof(QueryBuilderPageVM.SetTables) when !args.IsSuccessful: {
					Show(
						"При загрузке списка таблиц произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(QueryBuilderPageVM.SetTokens) when args.IsSuccessful: {
					Show(
						"Список токенов успешно загружен.",
						"Ошибка"
					);					
					break;
				}

				case nameof(QueryBuilderPageVM.SetTokens) when !args.IsSuccessful: {
					Show(
						"При загрузке списка токенов произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(QueryBuilderPageVM.SetTables) when args.IsSuccessful: break;

				case nameof(QueryBuilderPageVM.SetTables) when !args.IsSuccessful: {
					Show(
						"При загрузке таблиц произошла ошибка.",
						"Ошибка"
					);
					break;
				}

				case nameof(QueryBuilderPageVM.GetData) when args.IsSuccessful: break;

				case nameof(QueryBuilderPageVM.GetData) when !args.IsSuccessful: {
					Show(
						"При загрузке данных произошла ошибка.",
						"Ошибка"
					);
					break;
				}
			}
		}
	}
}
