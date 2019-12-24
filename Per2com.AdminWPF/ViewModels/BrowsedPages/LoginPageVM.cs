using System.Windows.Input;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class LoginPageVM : BrowsedPageVM<LoginPageVM>
	{
		public string Server { get; set; } = "rc1a-768ipvapujcui5yo.mdb.yandexcloud.net";

		public string Database { get; set; } = "per2Base";

		public string UserId { get; set; }

		public string Password { get; set; }

		public ICommand Login => new CommandDelegate {
			ExecuteDelegate = (x) => SetConnectionString()
		};

		public LoginPageVM() { }

		public void SetConnectionString()
		{
			if (Validator.Validate(this, true)) {
				MainWindowVM.Default.Bridge.TryConnect(nameof(SetConnectionString), Server, Database, UserId, Password, true);
			}
		}
	}
}
