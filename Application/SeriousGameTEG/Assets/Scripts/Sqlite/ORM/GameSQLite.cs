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

			/*
			this.games = this.sqlDB().ExecuteQuery("SELECT " + qutil.allModel(GameSQLite.table)+ " FROM "+GameUserSQLite.table
				+ qutil.join(GameSQLite.table,GameSQLite.Id,GameUserSQLite.table,GameUserSQLite.Id_Game)
				+" WHERE "+ qutil.equalsValue(GameUserSQLite.table,GameUserSQLite.Id_User,userManager.getUserSelected())
				+ " UNION "
				+ "SELECT "+ qutil.allModel(GameSQLite.table)+ " FROM "+ GameSQLite.table + " WHERE "
				+ qutil.equalsValue(GameSQLite.table,GameSQLite.Is_Default,1)
				);
			
			Debug.Log("Query return games #:" + this.games.Rows.Count);

			/*
			this.games = this.sqlDB().ExecuteQuery ("SELECT "+ qutil.allModel(GameSQLite.table) + ", "
                + qutil.attributeFromTable(CategorySQLite.table,CategorySQLite.Name) + " as "+ qutil.attributeFromTableTag(CategorySQLite.table,CategorySQLite.Name)
			    +" FROM " + GameSQLite.table + qutil.leftOuterJoin(LevelResultSQLite.table, LevelResultSQLite.Level_Code, GameSQLite.table, GameSQLite.Level_code_pass)
			    + qutil.join(CategorySQLite.table,CategorySQLite.Id,GameSQLite.table,GameSQLite.Id_Category)
			    + " WHERE " + qutil.equalsValue(GameSQLite.table,GameSQLite.Is_Default,1)
                + " or " + qutil.equalsValue(LevelResultSQLite.table, LevelResultSQLite.Id_User, userManager.getUserSelected())
			    + ";");

			/*
			this.games = this.sqlDB().ExecuteQuery ("SELECT "+ qutil.allModel(GameSQLite.table) + ", "
			    + qutil.attributeFromTable(CategorySQLite.table,CategorySQLite.Name) + " as "+ qutil.attributeFromTableTag(CategorySQLite.table,CategorySQLite.Name)
			    +" FROM " + GameSQLite.table + qutil.innerJoin(CategorySQLite.table, CategorySQLite.Id, GameSQLite.table, GameSQLite.Id_Category)
			    + ";");
			*/

			return this.Games!=null;
		}

		public List<int> getDependants (){
			return null;
		}

		public DataTable getLevelsOfGame (int IdUser,int IdGame){
			GameLevelSQLite GameLevelORM = new GameLevelSQLite ();
			return GameLevelORM.getLevelsOfGame (IdUser, IdGame);
		}

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}
}