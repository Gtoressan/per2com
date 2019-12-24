using System;
using System.Collections.Generic;
using System.Linq;

namespace Per2com.DataModel.Descriptors
{
	public class MySqlDescriptor : Descriptor
	{
		public MySqlDescriptor(Bridge bridge) : base(bridge) { }

		public override string[] GetTables(string tag)
		{
			var query = from i in Bridge.Select(tag, "show tables from per2Base")
						select i[0] as string;

			return query.ToArray();
		}

		public override DescriptionToken[] GetTokens(string tag, string table)
		{
			var query = from i in Bridge.Select(tag, "select COLUMN_NAME, DATA_TYPE, COLUMN_KEY, TABLE_NAME, COLUMN_COMMENT from information_schema.COLUMNS where TABLE_NAME = @table or TABLE_NAME in (select COLUMN_COMMENT from information_schema.COLUMNS where TABLE_NAME = @table)", ("@table", table))
						select new DescriptionToken {
							Name = (string)i[0],
							Type = ToType((string)i[1]),
							Kind = (string)i[3] == table ? DescriptionTokenKind.Inner : DescriptionTokenKind.Foreign,
							Table = (string)i[3],
							Comment = (string)i[4]
						};

			return query.ToArray();
		}

		public override object[][] Select(string tag, string table, DescriptionToken[] tokens)
		{
			List<string> foreignTables = new List<string>();
			List<string> usedColumns = new List<string>();
			List<(string name, object value)> prms = new List<(string name, object value)>();
			bool isFirstCondition = true;
			string begining = "select";
			string ending = " from " + table;
			string conditions = "";

			for (int i = 0; i < tokens.Length; ++i) {

				// Adds column name in selected column name set.
				if (!usedColumns.Contains(tokens[i].Table + "." + tokens[i].Name)) {
					begining += (i == 0 ? " " : ", ") + tokens[i].Table + "." + tokens[i].Name;
					usedColumns.Add(tokens[i].Name);
				}

				// Adds columns condition if it is.
				if (tokens[i].Condition != "без условий") {
					if (isFirstCondition) {
						conditions += $" where {tokens[i].Table}.{tokens[i].Name} {tokens[i].Condition} @var{i}";
						isFirstCondition = false;
					}
					else {
						conditions += $" and {tokens[i].Table}.{tokens[i].Name} {tokens[i].Condition} @var{i}";
					}

					prms.Add(($"@var{i}", tokens[i].Value));
				}

				// Adds joinings.
				if (tokens[i].Kind == DescriptionTokenKind.Foreign && !foreignTables.Contains(tokens[i].Table)) {
					ending += $" inner join {tokens[i].Table} on {table}.{tokens[i].Table}Id = {tokens[i].Table}.Id";
					foreignTables.Add(tokens[i].Table);
				}
			}

			return Bridge.Select(tag, begining + ending + conditions, prms.ToArray()).ToArray();
		}

		public override DescriptionTokenKind ToKind(string key)
		{
			switch (key) {
				default: throw new NotImplementedException();
				case "MUL": return DescriptionTokenKind.Foreign;
				case "PRI": return DescriptionTokenKind.Inner;
				case "": return DescriptionTokenKind.Inner;
			}
		}

		public override DescriptionTokenType ToType(string type)
		{
			switch (type) {
				default: throw new NotImplementedException();
				case "float": return DescriptionTokenType.Float;
				case "int": return DescriptionTokenType.Int;
				case "varchar": return DescriptionTokenType.String;
			}
		}
	}
}
