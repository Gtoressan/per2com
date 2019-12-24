using System.Windows;

namespace Per2com.AdminWPF.ViewModels
{
	public abstract class Validator<T>
	{
		public Validator() { }

		public abstract T GetCopy(T item);

		public abstract object[] GetId(T item);

		public abstract bool Validate(T item, bool showMessage);

		protected virtual void MayShow(bool show, string caption, string message, MessageBoxButton button)
		{
			if (show) {
				MessageBox.Show(message, caption, button);
			}
		}
	}
}
