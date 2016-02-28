using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;


[Serializable]
public class cubeJson : MonoBehaviour {

	public float x;
	public float y;

	[NonSerialized]
	public float z;


	void Awake (){
		JsonUtility.FromJsonOverwrite("{\"x\":2.0,\"y\":2.0}",this);
	}

	void Start(){
		Debug.Log(JsonUtility.ToJson(this));
		JsonUtility.FromJsonOverwrite("{\"x\":2.0,\"y\":2.0}",this);
		this.gameObject.transform.localScale = new Vector3 (this.x,this.y,0f);
		this.z = this.gameObject.transform.localScale.z;
	}

	public void setZ ( float a){
		this.z = a;
	}
}
