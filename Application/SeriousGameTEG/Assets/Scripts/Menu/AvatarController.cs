using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
*/

public class AvatarController : MonoBehaviour {

	public GameObject UserSelectionPanel;

	private string filepath;

	private SqliteDatabase sqlDB;
	
	void Awake() 
	{
		/*
		string dbPath = System.IO.Path.Combine (Application.persistentDataPath, "TEG_SG.db");
		var dbTemplatePath = System.IO.Path.Combine(Application.streamingAssetsPath, "TEG_SG.db");
		
		if (!System.IO.File.Exists(dbPath)) {
			Debug.Log ("#########################The Database was not initialized");
			// game database does not exists, copy default db as template
			if (Application.platform == RuntimePlatform.Android)
			{
				// Must use WWW for streaming asset
				WWW reader = new WWW(dbTemplatePath);
				while ( !reader.isDone) {}
				Debug.Log ("#########################Copying DataBase From Streaming Assets (Android)");
				System.IO.File.WriteAllBytes(dbPath, reader.bytes);
			} else {
				Debug.Log ("#########################Copying DataBase From Streaming Assets (NO Android)");
				System.IO.File.Copy(dbTemplatePath, dbPath, true);
			}       
		}
		*/
		/*
		Debug.Log("##################Before Create anything");
		sqlDB = new SqliteDatabase(dbPath);

		DataTable result = sqlDB.ExecuteQuery("SELECT * FROM user;");
		Debug.Log("ELEMENTOOOOOOOOOOOOOOOOOOOS " + result.Rows.Count );
		*/
		user users = new user();
		if(users.loadUsers()){
			foreach(DataRow current in users.Users.Rows){
				Debug.Log("###########"+current[user.Id]+"||"+current[user.Name]+"||"+current[user.LastName]);
			}
		}
	}
	

	public void ToggleUserSelectionPanel(){
		UserSelectionPanel.gameObject.SetActive(!UserSelectionPanel.gameObject.activeSelf);
	}

}
