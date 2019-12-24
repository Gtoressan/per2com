using System;

namespace Per2com.DataModel
{
	public abstract class Directory<T>
	{
		public Bridge Bridge { get; protected set; }

		public Directory(Bridge bridge) => Bridge = bridge ?? throw new ArgumentNullException(nameof(bridge));

		public abstract void Add(string tag, T item);

		public abstract void Drop(string tag, params object[] keys);

		public abstract void Edit(string tag, T item, params object[] keys);

		public abstract T Find(string tag, params object[] keys);

		public abstract T[] Get(string tag);
	}
}
