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
		public static string Level_code_pass = "level_code_pass";
		public static string Is_Default = "is_default";
		private SqliteDatabase sqlDBAttr = null;

		//QueryUtils qutil = new QueryUtils ();

		private DataTable games;

		public DataTable Games {
			get{ return this.games; }
		}

		public bool loadGames (int IdUser) {
			this.games = this.sqlDB().ExecuteQuery("select * from game left outer join enabled_game_user on game.id=enabled_game_user.id_game and enabled_game_user.id_user = "+IdUser+" where game.is_default = 1 or enabled_game_user.id_user not null");
			return this.Games!=null;
		}

		public bool loadAllGames (){
			this.games = this.sqlDB().ExecuteQuery("select game.name as name, game.id as id FROM game JOIN category on  game.id_category = category.id");
			return this.Games!=null;
		}

		public List<int> getDependants (){
			return null;
		}

		public List<GameLevelDTO> getLevelsOfGame (int IdUser,int IdGame){
			GameLevelSQLite GameLevelORM = new GameLevelSQLite ();
			return GameLevelORM.getLevelsOfGame (IdUser, IdGame);
		}

		public Game getGame(int Id_game){
			DataTable result = this.sqlDB().ExecuteQuery("SELECT * FROM game WHERE id=" + Id_game);
			if( result.Rows.Count != 1 ) return null;
			return new Game((int)result.Rows[0][GameSQLite.Id],(int)result.Rows[0][GameSQLite.Id_Category],
				(string)result.Rows[0][GameSQLite.Name], (string)result.Rows[0][GameSQLite.Description],
				(int)result.Rows[0][GameSQLite.Level_code_pass], 0<(int)result.Rows[0][GameSQLite.Is_Default]);
		}

		public List<Game> getAllGames(){
			DataTable result = this.sqlDB().ExecuteQuery("SELECT * FROM game");
			List<Game> games = new List<Game>();
			foreach( DataRow current in result.Rows ){
				games.Add(new Game((int)current[GameSQLite.Id], (int)current[GameSQLite.Id_Category],
					(string)current[GameSQLite.Name], (string)current[GameSQLite.Description],
					(int)current[GameSQLite.Level_code_pass], 0<(int)current[GameSQLite.Is_Default]));
			}
			return games;
		}

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}

	public class Game {
		public int Id;
		public int Id_Category;
		public string Name;
		public string Description;
		public int Level_code_pass;
		public bool Is_Default;

		public Game (int Id, int Id_Category, string Name, string Description, int Level_code_pass, bool isdef){
			this.Id = Id;
			this.Id_Category = Id_Category;
			this.Name=Name;
			this.Description=Description;
			this.Level_code_pass=Level_code_pass;
			this.Is_Default = isdef;
		}
	}
}