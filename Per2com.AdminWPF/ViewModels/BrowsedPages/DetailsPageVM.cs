using System.Windows.Input;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class DetailsPageVM<T> : BrowsedPageVM<T>
	{
		public T Item { get; protected set; }

		public ICommand Delete => new CommandDelegate {
			ExecuteDelegate = (x) => DropItem()
		};

		public ICommand Edit => new CommandDelegate {
			ExecuteDelegate = (x) => GoToEdit()
		};

		public DetailsPageVM(T item) => Item = item;

		public void DropItem()
		{
			Directory.Drop(nameof(DropItem), Validator.GetId(Item));
		}

		public void GoToEdit()
		{
			MainWindowVM.Default.GoTo(Handler.Paths["editPage"], Item);
		}
	}
}
