using System.Windows.Input;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class EditPageVM<T> : BrowsedPageVM<T>
	{
		public T OldItem { get; protected set; }

		public T Item { get; protected set; }

		public EditPageVM(T item) => Item = item;

		public ICommand Edit => new CommandDelegate {
			ExecuteDelegate = (x) => EditItem()
		};

		public void SetItem()
		{
			OldItem = Item;
			Item = Validator.GetCopy(Item);
		}

		public void EditItem()
		{
			if (Validator.Validate(Item, true)) {
				Directory.Edit(nameof(EditItem), Item, Validator.GetId(OldItem));
			}
		}
	}
}
