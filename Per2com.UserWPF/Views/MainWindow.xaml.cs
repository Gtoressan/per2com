using Per2com.UserWPF.ViewModels;
using System.Windows;

namespace Per2com.UserWPF.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = MainWindowVM.Default;
		}
	}
}
