using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Configuration_RN : MonoBehaviour, GameConfigurationInterface {

	public int amountNumbers;

	public void setConfiguration (string JsonConfigurations){
		JsonUtility.FromJsonOverwrite(JsonConfigurations,this);
		Debug.Log( "#####GameConfiguration jsonFromGC ["+ JsonUtility.ToJson(this) +"]" );
	}
	
}
