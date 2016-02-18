using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace ORM
{
	public class LevelResultSQLite {
		
		public static string table = "level_result";
		public static string Id_User = "id_user";
		public static string Level_Code = "level_code";
		private SqliteDatabase sqlDBAttr = null;

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}
}

