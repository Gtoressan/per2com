using System;
using System.Windows.Input;

namespace Per2com.AdminWPF.ViewModels
{
	public class CommandDelegate : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public Func<object, bool> CanExecuteDelegate { get; set; }

		public Action<object> ExecuteDelegate { get; set; }

		public CommandDelegate() { }

		protected void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanExecute(object parameter)
		{
			return CanExecuteDelegate?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			ExecuteDelegate?.Invoke(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged();
		}
	}
}
