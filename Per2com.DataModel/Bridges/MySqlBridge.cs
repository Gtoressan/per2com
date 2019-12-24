using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Per2com.DataModel.Bridges
{
	public class MySqlBridge : Bridge
	{
		public override void Execute(string tag, string queryText, params (string name, object value)[] parameters)
		{
			using (var connection = new MySqlConnection(ConnectionString)) {
				using (var command = new MySqlCommand(queryText, connection)) {
					foreach (var (name, value) in parameters) {
						command.Parameters.AddWithValue(name, value);
					}

					try {
						connection.Open();
						command.ExecuteNonQuery();
						OnGotResult(tag, queryText, null);
					}
					catch (Exception ex) {
						OnGotResult(tag, queryText, ex);
					}
					finally {
						connection.Close();
					}
				}
			}
		}

		public override IEnumerable<object[]> Select(string tag, string queryText, params (string name, object value)[] parameters)
		{
			using (var connection = new MySqlConnection(ConnectionString)) {
				using (var command = new MySqlCommand(queryText, connection)) {
					foreach (var (name, value) in parameters) {
						command.Parameters.AddWithValue(name, value);
					}

					MySqlDataReader reader;
					object[] values;

					try {
						connection.Open();
						reader = command.ExecuteReader();
					}
					catch (Exception ex) {
						OnGotResult(tag, queryText, ex);
						yield break;
					}

					while (reader.Read()) {
						values = new object[reader.FieldCount];
						reader.GetValues(values);
						yield return values;
					}

					connection.Close();
					OnGotResult(tag, queryText, null);
				}
			}
		}

		public override void TryConnect(string tag, string server, string database, string userId, string password, bool remember)
		{
			const string queryText = "attempt to connect";
			var builder = new MySqlConnectionStringBuilder {
				CharacterSet = "utf8",
				Database = database,
				Password = password,
				Server = server,
				UserID = userId
			};

			if (remember) {
				ConnectionString = builder.ToString();
			}

			using (var connection = new MySqlConnection(builder.ToString())) {
				try {
					connection.Open();
					OnGotResult(tag, queryText, null);
				}
				catch (Exception ex) {
					OnGotResult(tag, queryText, ex);
				}
				finally {
					connection.Close();
				}
			}
		}
	}
}
