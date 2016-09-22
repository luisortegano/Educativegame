using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Configuration_RN : MonoBehaviour, GameConfigurationInterface {

	public float challengeTime;
	public int amountNumbers;
	public int maxFails;

	[NonSerialized]
	public GameLevelPanel gameLevelPanel;

	public void setConfiguration (string JsonConfigurations){
		JsonUtility.FromJsonOverwrite(JsonConfigurations,this);
		Debug.Log( string.Format("#####GameConfiguration jsonFromGC challengeTime[{0}]amountNumbers[{1}]maxFails[{2}]", this.challengeTime,
			this.amountNumbers, this.maxFails));
	}

	public void setGameLevelPanel ( GameLevelPanel glp  ){
		this.gameLevelPanel = glp;
	}
	
}
