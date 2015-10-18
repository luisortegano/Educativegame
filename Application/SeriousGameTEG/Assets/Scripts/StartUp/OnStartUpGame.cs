using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[InitializeOnLoad]
public class Startup {

	public GameObject ConfigurationElement = new GameObject ();

	static Startup()
	{
		Debug.Log("StartUpScript Launched");
	}
}
