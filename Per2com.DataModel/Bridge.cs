using System;
using System.Collections.Generic;

namespace Per2com.DataModel
{
	public abstract class Bridge
	{
		public event Action<Bridge, BridgeEventArgs> GotResut;

		public string ConnectionString { get; set; }

		public Bridge() { }

		protected void OnGotResult(string tag, string queryText, Exception exception)
		{
			GotResut?.Invoke(this, new BridgeEventArgs(tag, queryText, exception));
		}

		public abstract void Execute(string tag, string queryText, params (string name, object value)[] parameters);

		public abstract IEnumerable<object[]> Select(string tag, string queryText, params (string name, object value)[] parameters);

		public abstract void TryConnect(string tag, string server, string database, string userId, string password, bool remember);
	}
}
