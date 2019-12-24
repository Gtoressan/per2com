using System;

namespace Per2com.DataModel
{
	public class DescriptionToken : ICloneable
	{
		public string Name { get; set; }

		public string Table { get; set; }

		public DescriptionTokenType Type { get; set; }

		public DescriptionTokenKind Kind { get; set; }

		public string Comment { get; set; }

		public string Condition { get; set; }

		public object Value { get; set; }

		public DescriptionToken() { }

		public override string ToString()
		{
			return $"{Name} ({Table})";
		}

		public object Clone()
		{
			return new DescriptionToken {
				Name = Name,
				Table = Table,
				Type = Type,
				Kind = Kind,
				Comment = Comment,
				Condition = Condition,
				Value = Value
			};
		}
	}
}