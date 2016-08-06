using UnityEngine;
using System.Collections.Generic;
using Persistence;

public class Configuration : MonoBehaviour {
	void Awake () {
		string[] initFiles = {"TEG_SG.db", "d3.min.js", "home.html"} ;
		foreach(string current in initFiles){
			if (!System.IO.File.Exists(System.IO.Path.Combine (Application.persistentDataPath, current))) {
				Debug.Log ("#####not initialized: " + current);
				if (Application.platform == RuntimePlatform.Android)
				{
					WWW reader = new WWW(System.IO.Path.Combine(Application.streamingAssetsPath,current));
					while (!reader.isDone) {}
					Debug.Log ("#####Copying From Streaming Assets: " + current);
					System.IO.File.WriteAllBytes(System.IO.Path.Combine (Application.persistentDataPath, current), reader.bytes);
				}     
			}
		}
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
