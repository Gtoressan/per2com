using System.Windows.Input;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class CreatePageVM<T> : BrowsedPageVM<T> where T: new()
	{
		public T Item { get; protected set; }

		public ICommand Create => new CommandDelegate {
			ExecuteDelegate = (x) => AddItem()
		};

		public CreatePageVM() { }

		public void SetItem() => Item = Validator.GetCopy(new T());

		public void AddItem()
		{
			if (Validator.Validate(Item, true)) {
				Directory.Add(nameof(AddItem), Item);
			}
		}
	}
}
