using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System;

[Serializable]
public class GameConfigurationJson : MonoBehaviour {
    /* Cantidad distintas de figuras que hay en el juego */
	public int AmountFigures;
	
	/* Cantidad que debe descubrir en el juego*/
	public int maxDiscoverFigures;
	
	/* Cantidad posible de fallos */
	public int maxFails;
	
	/* Tiempo del juego */
	public int challengeTime;

	void Start(){

		//this.AmountFigures = 8;
		//this.maxDiscoverFigures = 5;
		//this.maxFails = 2;
		//this.challengeTime = 50;

		Debug.Log("Old Configs" + JsonUtility.ToJson(this));

		JsonUtility.FromJsonOverwrite("{\"AmountFigures\":8,\"maxDiscoverFigures\":5,\"maxFails\":-2,\"challengeTime\":50}", this);

		Debug.Log("New Configs" + JsonUtility.ToJson(this));
		//JsonUtility.FromJsonOverwrite("{\"x\":2.0,\"y\":2.0}",this);
		//this.gameObject.transform.localScale = new Vector3 (this.x,this.y,0f);
		//this.z = this.gameObject.transform.localScale.z;

	}
}