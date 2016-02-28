using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;


public class cubeJsonB : MonoBehaviour {

	public float x;
	public float y;

	[NonSerialized]
	public float z;


	void Awake (){
		
		this.x = this.gameObject.transform.localScale.x;
		this.y = this.gameObject.transform.localScale.y;
		this.z = this.gameObject.transform.localScale.z;
	}

	void Start(){
		Debug.Log(JsonUtility.ToJson(this));
	}
}
