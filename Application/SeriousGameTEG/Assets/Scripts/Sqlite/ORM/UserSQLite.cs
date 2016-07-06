using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace ORM {
	public class UserSQLite {
		
		public static string table = "user";
		public static string Id = "id";
		public static string Name = "name";
		public static string LastName = "lastname";
		public static string PROFILE_IMAGE_PATH = Application.persistentDataPath + Path.AltDirectorySeparatorChar ;
		private SqliteDatabase sqlDBAttr = null;
		//+ "users" + Path.AltDirectorySeparatorChar + "profile" + Path.AltDirectorySeparatorChar;
		//Path.Combine(Path.Combine(Application.persistentDataPath,"users"),"profile")+Path.AltDirectorySeparatorChar;

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}

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

		public string imagePathOfUser (int UserId){
			return UserSQLite.PROFILE_IMAGE_PATH + UserId + ".png";
		}

		public User getUserData ( int idUser ){
			DataTable userTable = this.sqlDB().ExecuteQuery("SELECT * FROM " + table +" WHERE user.id = " + idUser);
			User user = new User ();

			foreach (DataRow current in userTable.Rows) {
				Debug.Log(current.ToString());
				user.Id = (int)current[Id];
				user.Name = (string)current[Name];
				user.LastName = (string)current[LastName];
			}

			return user;
		}

	}

	public class User {
		public int Id;
		public string Name;
		public string LastName;

		public override string ToString (){
			return Id + " " + Name + " " + LastName + " ";
		}
	}
}