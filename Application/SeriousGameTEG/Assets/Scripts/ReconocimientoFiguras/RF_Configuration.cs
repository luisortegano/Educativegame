using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System;

[Serializable]
public class RF_Configuration : MonoBehaviour {
	
	/* Cantidad distintas de figuras que hay en el juego */
	public int AmountFigures;
	
	/* Cantidad que debe descubrir en el juego*/
	public int maxDiscoverFigures;
	
	/* Cantidad posible de fallos */
	public int maxFails;
	
	/* Tiempo del juego */
	public int challengeTime;

	[NonSerialized]
	public GameLevelPanel gameLevelPanel;
	
	
	
	/* results */
	int expendedTime ;
	int hits;
	int fails;
	bool winGame;
	string message;
	
	public void calculateExpendedTime(int restantTime){
		this.expendedTime = this.challengeTime-restantTime;
	}
	
	public void setHits (int hits){
		this.hits = hits;
	}
	
	public void setFails (int fails){
		this.fails = fails;
	}
	
	public string getResultMessage (){
		return this.message;
	}
	
	public void verifiedResult (){
		if ( expendedTime <= 0){
			message = "Ups! Se acabo el tiempo pequeño";
			this.winGame = false;
			return;
		}
		
		if( 0 < this.maxFails && maxFails <= fails ){
			message = "Wooot! esta vez nuestros dedos nos traicionaron demasiado";
			this.winGame = false;
			return;
		}
		
		if ( hits == maxDiscoverFigures  ){
		message = "Definitivamente somos buenos en esto!";
			this.winGame = true;
			return;
		}
		this.winGame = false;
	}

	public void setConfiguration (string JsonConfigurations){
		Debug.Log( "Configuration to use ["+JsonConfigurations+"]" );
        
		GameConfigurationJson configuration = JsonUtility.FromJson<GameConfigurationJson>(JsonConfigurations);
		
        Debug.Log( "Configuration readed and reversed [" + JsonUtility.ToJson(configuration) + "]");
        
        this.AmountFigures=configuration.AmountFigures;
        this.maxDiscoverFigures=configuration.maxDiscoverFigures;
        this.maxFails=configuration.maxFails;
		this.challengeTime=configuration.challengeTime;
		printValues();
	}

	public void setGameLevelPanel ( GameLevelPanel glp  ){
		this.gameLevelPanel = glp;
	}

	void printValues(){
		Debug.Log("Configuration Values for Game: ["+AmountFigures +" " +maxDiscoverFigures+" " + maxFails + " " + challengeTime + "]");
	}
	
}
