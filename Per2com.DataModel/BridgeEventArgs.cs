using System;

namespace Per2com.DataModel
{
	public class BridgeEventArgs : EventArgs
	{
		public string Tag { get; protected set; }

		public string QueryText { get; protected set; }

		public Exception Exception { get; protected set; }

		public bool IsSuccessful => Exception is null ? true : false;

		public BridgeEventArgs(string tag, string queryText, Exception exception)
		{
			Tag = tag;
			QueryText = queryText;
			Exception = exception;
		}
	}
}