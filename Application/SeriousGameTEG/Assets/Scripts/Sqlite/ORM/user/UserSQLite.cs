using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class UserSQLite {

	public static string table = "user";
	public static string Id = "id";
	public static string Name = "name";
	public static string LastName = "lastname";

	private DataTable users;

	public DataTable Users {
		get{ return this.users; }
	}

	public bool loadUsers (){
		SqliteDatabase sqlDB = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
		this.users = sqlDB.ExecuteQuery("SELECT * FROM "+UserSQLite.table+";");
		return this.Users!=null;
	}


}
