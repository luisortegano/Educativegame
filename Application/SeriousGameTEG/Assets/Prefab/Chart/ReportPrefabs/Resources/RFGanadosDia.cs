using ORM;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RFGanadosDia : MonoBehaviour {

	private int Game = 1;
	private Game game;
	private List<GameLevelDTO> levels;
	private List<Toggle> LevelUI;

	private void addLayoutElement( int Heigth ){
		
	}

	public void Awake(){
		// Get game Data
		GameSQLite gamesql = new GameSQLite();
		game = gamesql.getGame(this.Game);

		// Get Levels of game
		GameLevelSQLite levelSQL = new GameLevelSQLite();
		levels = levelSQL.getAllLevelsOfGame(this.Game);

//		LevelUI = new List<Toggle>();
//		Toggle auxiliar;
//		//Add checkbox per level
//		foreach(GameLevelDTO current in levels){
//			auxiliar = new Toggle();
//			auxiliar.GetComponent<Text>().text = current.Level.ToString();
//			LayoutElement le = auxiliar.gameObject.AddComponent<LayoutElement>();
//			le.preferredHeight = 30;
//
//		}
	}

}
