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

		QueryUtils qutil = new QueryUtils ();

		private DataTable games;

		public DataTable Games {
			get{ return this.games; }
		}

		public bool loadGames () {
			SqliteDatabase sqlDB = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));

			/*
			Debug.Log ("SELECT " + qutil.allModel (GameSQLite.table) + ", "
			           + qutil.attributeFromTable (CategorySQLite.table, CategorySQLite.Name) + " as " + qutil.attributeFromTableTag (CategorySQLite.table, CategorySQLite.Name) +
			           " FROM " + GameSQLite.table + qutil.innerJoin (CategorySQLite.table, CategorySQLite.Id, GameSQLite.table, GameSQLite.Id_Category) + ";");
			*/
			this.games = sqlDB.ExecuteQuery ("SELECT "+ qutil.allModel(GameSQLite.table) + ", "
			                                 + qutil.attributeFromTable(CategorySQLite.table,CategorySQLite.Name) + " as "+ qutil.attributeFromTableTag(CategorySQLite.table,CategorySQLite.Name)+
			    " FROM " + GameSQLite.table + qutil.innerJoin(CategorySQLite.table, CategorySQLite.Id, GameSQLite.table, GameSQLite.Id_Category) + ";");

			return this.Games!=null;
		}

		public List<int> getDependants (){
			return null;
		}


	}
}