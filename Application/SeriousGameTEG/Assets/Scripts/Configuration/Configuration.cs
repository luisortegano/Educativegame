using UnityEngine;
using System.Collections.Generic;
using Persistence;

public class Configuration : MonoBehaviour {
	void Awake () {
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
	}
}
