using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace ORM
{
	public class LevelResultsSQLite {
		
		public static string table = "level_results";
		public static string Id_User = "id_user";
		public static string Level_Code = "level_code";
		public static string Win = "win";
		public static string Result = "result";
		private SqliteDatabase sqlDBAttr = null;

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
		
		public void insertLevelResult (int IdUser, int LevelCode, bool win, string resultJson){
			printAllLevelResult();
			this.sqlDB().ExecuteNonQuery("INSERT INTO "+LevelResultsSQLite.table+" VALUES ("+IdUser+","+LevelCode+","
				+(win?1:0)+", '"+ resultJson +"');");
		}

		/* Retorna todos los resultados de un [juego,nivel,usuario] */
		public DataTable getLevelResults (int UserId, int GameId, int Level){
			return this.sqlDB().ExecuteQuery("select * from level_results join user on level_results.id_user = user.id and user.id = "+ UserId 
				+ " join game_level on level_results.level_code = game_level.code and game_level.level = "+ Level 
				+ " join game on game_level.id_game = game.id and game.id = " + GameId);
		}
		
		public void printAllLevelResult (){
			DataTable dt = this.sqlDB().ExecuteQuery("SELECT * FROM "+LevelResultsSQLite.table+";");
			foreach( DataRow dr in dt.Rows ){
				Debug.Log("("+ dr[LevelResultsSQLite.Id_User] +","+ dr[LevelResultsSQLite.Level_Code] +")");
			}
		}
	}
}

