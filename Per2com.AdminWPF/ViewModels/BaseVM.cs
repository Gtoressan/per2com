using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Per2com.AdminWPF.ViewModels
{
	public abstract class BaseVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public BaseVM Owner { get; set; }

		public BaseVM() { }

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetValue<T>(ref T field, T valies,[CallerMemberName] string propertyName = "")
		{
			field = valies;
			OnPropertyChanged(propertyName);
		}
	}
}
