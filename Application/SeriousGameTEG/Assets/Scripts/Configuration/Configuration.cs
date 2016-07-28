using UnityEngine;
using System.Collections.Generic;
using Persistence;

public class Configuration : MonoBehaviour {
	void Awake () {
		string dbPath = System.IO.Path.Combine (Application.persistentDataPath, "TEG_SG.db");
		var dbTemplatePath = System.IO.Path.Combine(Application.streamingAssetsPath, "TEG_SG.db");
        
        if (!System.IO.File.Exists(dbPath)) {
			Debug.Log ("#####The Database was not initialized");
			// game database does not exists, copy default db as template
			if (Application.platform == RuntimePlatform.Android)
			{
				// Must use WWW for streaming asset
				WWW reader = new WWW(dbTemplatePath);
				while ( !reader.isDone) {}
				Debug.Log ("#####Copying DataBase From Streaming Assets (Android)");
				System.IO.File.WriteAllBytes(dbPath, reader.bytes);
			} else {
				Debug.Log ("#####Copying DataBase From Streaming Assets (NO Android)");
				System.IO.File.Copy(dbTemplatePath, dbPath, true);
			}       
		}

		// Copy D3js lib
		if (!System.IO.File.Exists(System.IO.Path.Combine (Application.persistentDataPath, "d3.min.js"))) {
			Debug.Log ("#####d3.min.js was not in local folder");
			// game database does not exists, copy default db as template
			if (Application.platform == RuntimePlatform.Android)
			{
				WWW reader = new WWW(System.IO.Path.Combine(Application.streamingAssetsPath, "d3.min.js"));
				while (!reader.isDone) {}
				Debug.Log ("##### d3.min.js.file -> " + System.IO.Path.Combine(Application.persistentDataPath, "d3.min.js"));
				System.IO.File.WriteAllBytes(System.IO.Path.Combine (Application.persistentDataPath, "d3.min.js"), reader.bytes);
			} else {
				Debug.Log ("#####Copying DataBase From Streaming Assets (NO Android)");
				System.IO.File.Copy(System.IO.Path.Combine(Application.streamingAssetsPath, "d3.min.js"),
					System.IO.Path.Combine (Application.persistentDataPath, "d3.min.js"), true);
			}       
		}

		if (!System.IO.File.Exists(System.IO.Path.Combine (Application.persistentDataPath, "home.html"))) {
			Debug.Log ("#####d3.min.js was not in local folder");
			// game database does not exists, copy default db as template
			if (Application.platform == RuntimePlatform.Android)
			{
				WWW reader = new WWW(System.IO.Path.Combine(Application.streamingAssetsPath, "home.html"));
				while (!reader.isDone) {}
				Debug.Log ("##### d3.min.js.file -> " + System.IO.Path.Combine(Application.persistentDataPath, "home.html"));
				System.IO.File.WriteAllBytes(System.IO.Path.Combine (Application.persistentDataPath, "home.html"), reader.bytes);
			} else {
				Debug.Log ("#####Copying DataBase From Streaming Assets (NO Android)");
				System.IO.File.Copy(System.IO.Path.Combine(Application.streamingAssetsPath, "home.html"),
					System.IO.Path.Combine (Application.persistentDataPath, "home.html"), true);
			}       
		}
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
