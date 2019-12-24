namespace Per2com.AdminWPF.ViewModels
{
	public abstract class Filtrator<T>
	{
		public Filtrator() { }

		public abstract T[] Filter(T[] array);
	}
}
