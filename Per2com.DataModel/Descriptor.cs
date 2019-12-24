using System;

namespace Per2com.DataModel
{
	public abstract class Descriptor
	{
		public Bridge Bridge { get; protected set; }

		public Descriptor(Bridge bridge) => Bridge = bridge ?? throw new ArgumentNullException(nameof(bridge));

		public abstract string[] GetTables(string tag);

		public abstract DescriptionToken[] GetTokens(string tag, string table);

		public abstract object[][] Select(string tag, string table, DescriptionToken[] tokens);

		public abstract DescriptionTokenKind ToKind(string key);

		public abstract DescriptionTokenType ToType(string type);
	}
}
