using System.Windows;
using System.Windows.Input;

using static System.Windows.MessageBox;
using static System.Windows.MessageBoxButton;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class IndexPageVM<T> : BrowsedPageVM<T>
	{
		T[] source;

		public T[] Source {
			get => source;
			protected set => SetValue(ref source, value);
		}

		public T SelectedItem { get; set; }

		public ICommand Delete => new CommandDelegate {
			ExecuteDelegate = (x) => DropItem()
		};

		public ICommand Edit => new CommandDelegate {
			ExecuteDelegate = (x) => GoToEdit()
		};

		public ICommand Filter => new CommandDelegate {
			ExecuteDelegate = (x) => FilterSource()
		};

		public IndexPageVM() { }

		public void SetSource()
		{
			Source = Directory.Get(nameof(SetSource));
		}

		public void DropItem()
		{
			if (SelectedItem != null) {
				if (Show("Запись будет немедленно удалена, вы уверены что хотите удалить запись?", "Сообщение", YesNo) == MessageBoxResult.Yes) {
					Directory.Drop(nameof(DropItem), Validator.GetId(SelectedItem));
				}
			}
		}

		public void FilterSource()
		{
			var array = Directory.Get(nameof(FilterSource));
			Source = Filtrator.Filter(array);
		}

		public void GoToDetails()
		{
			if (SelectedItem != null) {
				MainWindowVM.Default.GoTo(Handler.Paths["detailsPage"], SelectedItem);
			}
		}

		public void GoToEdit()
		{
			if (SelectedItem != null) {
				MainWindowVM.Default.GoTo(Handler.Paths["editPage"], SelectedItem);
			}
		}
	}
}
