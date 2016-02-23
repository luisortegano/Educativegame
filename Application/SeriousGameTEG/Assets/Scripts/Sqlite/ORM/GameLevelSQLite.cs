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
		
		//QueryUtils qutil = new QueryUtils ();

		public DataTable getLevelsOfGame (int IdUser, int IdGame ){
			return this.sqlDB ().ExecuteQuery ("SELECT * FROM game_level LEFT OUTER JOIN game_level_user ON game_level_user.id_user = "+IdUser+" AND game_level_user.game_level_code = game_level.code WHERE (game_level_user.id_user NOT null OR level=1) AND game_level.id_game = "+IdGame+";");
		}

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}
}