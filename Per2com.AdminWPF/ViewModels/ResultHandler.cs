using Per2com.DataModel;
using System.Collections.Generic;

namespace Per2com.AdminWPF.ViewModels
{
	public abstract class ResultHandler
	{
		public object DataContext { get; set; }

		public abstract Dictionary<string, string> Paths { get; }

		public ResultHandler() { }

		public abstract void Handler(Bridge bridge, BridgeEventArgs args);
	}
}
