using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace ORM {
	public class GameLevelSQLite{
		
		public static string table = "game_level";
		public static string code = "code";
		public static string Id_Game = "id_game";
		public static string Level = "level";
		public static string Configuration = "configuration";
		private SqliteDatabase sqlDBAttr = null;
		
		QueryUtils qutil = new QueryUtils ();

		public DataTable getLevelsOfGame ( int IdGame ){
			return this.sqlDB ().ExecuteQuery ("SELECT * from "+GameLevelSQLite.table+" where "+ qutil.equalsValue(GameLevelSQLite.table, GameLevelSQLite.Id_Game, IdGame ) +";");
		}

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}
}