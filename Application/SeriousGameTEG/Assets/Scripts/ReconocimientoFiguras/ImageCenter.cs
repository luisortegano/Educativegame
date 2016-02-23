using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;


public class ImageCenter : MonoBehaviour {
	
	public string basePath =  "Images/Figuras/";
	public List<string> ImagesURI;
	
	public string getRandomImage (){
		System.Random r = new System.Random();
		return Path.Combine( Application.dataPath ,Path.Combine(basePath, ImagesURI[r.Next(0,ImagesURI.Count)]));
	}
	
	public void getAssetsBundlesName (){
		var names = AssetDatabase.GetAllAssetBundleNames();
		foreach (var name in names)
		Debug.Log ("AssetBundle: " + name);
	}
	
	void Start (){
		this.getAssetsBundlesName();
	}
	
}
