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
			this.sqlDB().ExecuteNonQuery("INSERT INTO "+LevelResultsSQLite.table+" VALUES ("+IdUser+","+LevelCode+","
				+(win?1:0)+", '"+ resultJson +"');");
		}

		/* Retorna todos los resultados de un [juego,nivel,usuario] */
//		public DataTable getLevelResults (int UserId, int GameId, int Level){
//			return this.sqlDB().ExecuteQuery("select * from level_results join user on level_results.id_user = user.id and user.id = "+ UserId 
//				+ " join game_level on level_results.level_code = game_level.code and game_level.level = "+ Level 
//				+ " join game on game_level.id_game = game.id and game.id = " + GameId);
//		}

		public List<LevelResult> getLevelResults (int UserId, int GameId, int Level){
			DataTable result = this.sqlDB().ExecuteQuery("select * from level_results join user on level_results.id_user = user.id and user.id = "+ UserId 
				+ " join game_level on level_results.level_code = game_level.code and game_level.level = "+ Level 
				+ " join game on game_level.id_game = game.id and game.id = " + GameId);

			List<LevelResult> results = new List<LevelResult>();
			foreach(DataRow current in result.Rows){
				results.Add( new LevelResult( 
					(int)current[LevelResultsSQLite.Id_User], 
					(int)current[LevelResultsSQLite.Level_Code] ,
					(int)current[LevelResultsSQLite.Win], 
					(string)current[LevelResultsSQLite.Result] 
				));
			}
			return results;
		}
	}

	public class LevelResult {
		public int IdUser;
		public int LevelCode;
		public bool Win;
		public LevelResultJson Result;

		public LevelResult (int IdUser, int LevelCode, int Win, string Result){
			this.IdUser=IdUser;
			this.LevelCode=LevelCode;
			this.Win=0<Win;
			this.Result=JsonUtility.FromJson<LevelResultJson>(Result);
		}

		public override string ToString ()
		{
			return "[LevelResult]=idUser["+IdUser+"]levelCode["+LevelCode+"]";
		}
	}
}

