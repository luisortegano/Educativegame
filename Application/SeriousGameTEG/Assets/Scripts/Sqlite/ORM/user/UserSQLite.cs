using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class UserSQLite {

	public static string table = "user";
	public static string Id = "id";
	public static string Name = "name";
	public static string LastName = "lastname";
	public static string PROFILE_IMAGE_PATH = Path.Combine(Path.Combine(Application.persistentDataPath,"users"),
	                                                       "profile")+Path.DirectorySeparatorChar;

	private DataTable users;

	public DataTable Users {
		get{ return this.users; }
	}

	public bool loadUsers (){
		SqliteDatabase sqlDB = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
		this.users = sqlDB.ExecuteQuery("SELECT * FROM "+UserSQLite.table+";");
		return this.Users!=null;
	}

	public int SaveUser ( string Name, string LastName ){
		SqliteDatabase sqlDB = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
		sqlDB.ExecuteNonQuery("insert into user (name, lastname) values ('"+Name+"','"+ LastName+"');");
		DataTable result = sqlDB.ExecuteQuery("select seq from sqlite_sequence where name = '"+UserSQLite.table+"';");
		if (result.Rows.Count == 1){
			return Convert.ToInt32(result.Rows[0]["seq"]);
		}
		return -1;
	}


}
