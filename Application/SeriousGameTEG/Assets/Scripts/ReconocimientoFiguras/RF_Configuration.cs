using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;
using ORM;


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
		this.expendedTime = Math.Abs(restantTime-challengeTime);
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
		if ( challengeTime - expendedTime <= 0){
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
		JsonUtility.FromJsonOverwrite(JsonConfigurations,this);
	}

	public void setGameLevelPanel ( GameLevelPanel glp  ){
		this.gameLevelPanel = glp;
	}

	public void persistResults (){
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		lr.insertLevelResult(um.getUserSelected(), this.gameLevelPanel.CodeLevel);
	}

	void printValues(){
		Debug.Log("Configuration Values for Game: ["+AmountFigures +" " +maxDiscoverFigures+" " + maxFails + " " + challengeTime + "]");
		if(this.gameLevelPanel != null){
			Debug.Log("LevelCode:"+this.gameLevelPanel.CodeLevel+"|GameId:"+this.gameLevelPanel.GameId);
		}
	}
	
}
