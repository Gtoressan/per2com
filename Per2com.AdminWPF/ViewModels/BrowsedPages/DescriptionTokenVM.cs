using Per2com.DataModel;
using System;

namespace Per2com.AdminWPF.ViewModels.BrowsedPages
{
	public class DescriptionTokenVM : BaseVM
	{
		DescriptionToken token;

		public int Id { get; set; }

		public DescriptionToken[] Tokens { get; set; }

		public DescriptionToken Token {
			get => token;
			set {
				SetValue(ref token, value);
				OnPropertyChanged("Conditions");
			}
		}

		public string[] Conditions {
			get {
				switch (Token.Type) {
					default: throw new NotImplementedException();
					case DescriptionTokenType.Float: return new string[] { "=", "!=", "<", ">", "без условий" };
					case DescriptionTokenType.Int: return new string[] { "=", "!=", "<", ">", "без условий" };
					case DescriptionTokenType.String: return new string[] { "=", "!=", "без условий" };
				}
			}
		}
	}
}
