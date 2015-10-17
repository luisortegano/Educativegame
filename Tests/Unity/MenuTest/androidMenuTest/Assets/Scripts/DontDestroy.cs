using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	public static DontDestroy singletonInstance;

	// Use this for initialization
	void Awake () {
		if(singletonInstance){
			DestroyImmediate(gameObject);
		}else
		{
			DontDestroyOnLoad(gameObject);
			singletonInstance = this;
		}

		//DontDestroyOnLoad(gameObject);
	}

}
