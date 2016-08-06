using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace ORM {
	public class GameLevelSQLite{
		
		public static string table = "game_level";
		public static string Code = "code";
		public static string Id_Game = "id_game";
		public static string Level = "level";
		public static string Configuration = "configuration";
		private SqliteDatabase sqlDBAttr = null;
		
		//QueryUtils qutil = new QueryUtils ();

		private List<GameLevelDTO> extractList ( DataTable table ){
			List<GameLevelDTO> levels = new List<GameLevelDTO>();
			foreach(DataRow current in table.Rows){
				levels.Add(new GameLevelDTO(
					(int)current[GameLevelSQLite.Code],(int)current[GameLevelSQLite.Id_Game],
					(int)current[GameLevelSQLite.Level],(string)current[GameLevelSQLite.Configuration]));
			}
			return levels;
		}

		public List<GameLevelDTO> getLevelsOfGame (int IdUser, int IdGame ){
			return extractList(this.sqlDB ().ExecuteQuery ("SELECT * FROM game_level LEFT OUTER JOIN game_level_user ON game_level_user.id_user = "+IdUser+" AND game_level_user.game_level_code = game_level.code WHERE (game_level_user.id_user NOT null OR level=1) AND game_level.id_game = "+IdGame+";"));
		}

		public List<GameLevelDTO> getAllLevelsOfGame (int IdGame ){
			return extractList(this.sqlDB ().ExecuteQuery ("SELECT * FROM game_level where game_level.id_game = " + IdGame ));
		}

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}

	public class GameLevelDTO {
		public int Code;
		public int Id_Game;
		public int Level;
		public string Configuration;

		public GameLevelDTO (int code, int id_game, int level, string conf){
			this.Code = code;
			this.Id_Game = id_game;
			this.Level = level;
			this.Configuration = conf;
		}
	} 
}