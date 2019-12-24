using Per2com.DataModel;
using System.Windows.Controls;

namespace Per2com.AdminWPF.ViewModels
{
	public abstract class BrowsedPageVM<T> : BaseVM
	{
		public ResultHandler Handler { get; set; }

		public Directory<T> Directory { get; set; }

		public Validator<T> Validator { get; set; }

		public Filtrator<T> Filtrator { get; set; }

		public BrowsedPageVM() { }

		public virtual Page Bind(Page page, BaseVM owner, Bridge bridge, ResultHandler handler, Directory<T> directory, Validator<T> validator, Filtrator<T> filtrator)
		{
			Owner = owner;
			Handler = handler;
			Directory = directory;
			Validator = validator;
			Filtrator = filtrator;
			page.DataContext = this;

			if (Handler != null) {
				Handler.DataContext = this;
			}

			if (bridge != null && Handler != null) {
				bridge.GotResut += Handler.Handler;
			}

			if (Directory != null && Handler != null) {
				Directory.Bridge.GotResut += Handler.Handler;
			}

			return page;
		}

		public virtual void Untie(Bridge bridge)
		{
			if (Directory != null && Handler != null) {
				Directory.Bridge.GotResut -= Handler.Handler;
			}

			if (bridge != null && Handler != null) {
				bridge.GotResut -= Handler.Handler;
			}

			if (Handler != null) {
				Handler.DataContext = null;
			}

			Filtrator = null;
			Validator = null;
			Directory = null;
			Handler = null;
			Owner = null;
		}
	}
}
