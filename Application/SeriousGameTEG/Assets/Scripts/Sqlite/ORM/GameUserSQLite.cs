using System;
using System.IO;
using UnityEngine;

namespace ORM
{
	public class GameUserSQLite
	{
		public static string table = "game_user";
		public static string Id_User = "id_user";
		public static string Id_Game = "id_game";
		private SqliteDatabase sqlDBAttr = null;
		
		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}
	}
}

