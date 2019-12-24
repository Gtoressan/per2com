using System;
using System.Collections.Generic;
using Per2com.AdminWPF.ViewModels;
using Per2com.AdminWPF.ViewModels.BrowsedPages;
using Per2com.DataModel;

using static System.Windows.MessageBox;

namespace Per2com.AdminWPF.ResultHandlers
{
	public class LoginPageVMHandler : ResultHandler
	{
		public override Dictionary<string, string> Paths => new Dictionary<string, string> {
			["startPage"] = "Manufacturer/IndexPage"
		};

		public override void Handler(Bridge bridge, BridgeEventArgs args)
		{
			switch (args.Tag) {
				default: throw new NotImplementedException();

				case nameof(LoginPageVM.SetConnectionString) when args.IsSuccessful: {
					MainWindowVM.Default.GoTo(Paths["startPage"], null);
					break;
				}

				case nameof(LoginPageVM.SetConnectionString) when !args.IsSuccessful: {
					Show(
						$"При авторизации произошла ошибка.",
						"Ошибка"
					);
					break;
				}
			}
		}
	}
}
