using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace ORM {
	public class GameSQLite{

		public static string table = "game";
		public static string Id = "id";
		public static string Id_Category = "id_category";
		public static string Name = "name";
		public static string Description = "description";

		private DataTable games;

		public DataTable Games {
			get{ return this.games; }
		}

		public bool loadGames () {
			SqliteDatabase sqlDB = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			this.games = sqlDB.ExecuteQuery ("SELECT * FROM " + GameSQLite.table + ";");
			return this.Games!=null;
		}

		public List<int> getDependants (){
			return null;
		}


	}
}