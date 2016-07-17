using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ORM {
	public class ReportSQLite {
		public static string table = "report";
		public static string Id = "id";
		public static string Name = "name";
		public static string Description = "description";
		public static string Prefab = "prefab_name";
		private SqliteDatabase sqlDBAttr = null;

		public SqliteDatabase sqlDB (){
			if (sqlDBAttr == null) {
				sqlDBAttr = new SqliteDatabase(Path.Combine (Application.persistentDataPath, "TEG_SG.db"));
			}
			return sqlDBAttr;
		}

		public List<ReportDTO> getAllReports (){
			DataTable result = this.sqlDB().ExecuteQuery("SELECT * FROM " + ReportSQLite.table);
			List<ReportDTO> listReport = new List<ReportDTO> ();
			foreach(DataRow current in result.Rows){
				listReport.Add(new ReportDTO((int)current[ReportSQLite.Id], (string)current[ReportSQLite.Name], (string)current[ReportSQLite.Description], (string)current[ReportSQLite.Prefab]));
			}
			return listReport;
		}

	}

	public class ReportDTO : Report {
		public ReportDTO (int Id, string Name, string Description, string Prefab){
			this.Id = Id;
			this.Name = Name;
			this.Description = Description;
			this.Prefab = Prefab;
		} 
	}
}