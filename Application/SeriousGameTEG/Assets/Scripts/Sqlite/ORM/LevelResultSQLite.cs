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
		private SqliteDatabase sqlDBAttr = null;

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
		
		public void insertLevelResult (int IdUser, int LevelCode){
			printAllLevelResult();
			this.sqlDB().ExecuteNonQuery("INSERT INTO "+LevelResultsSQLite.table+" VALUES ("+IdUser+","+LevelCode+");");
		}
		
		public void printAllLevelResult (){
			DataTable dt = this.sqlDB().ExecuteQuery("SELECT * FROM "+LevelResultsSQLite.table+";");
			foreach( DataRow dr in dt.Rows ){
				Debug.Log("("+ dr[LevelResultsSQLite.Id_User] +","+ dr[LevelResultsSQLite.Level_Code] +")");
			}
		}
	}
}

