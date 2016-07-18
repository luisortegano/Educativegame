using ORM;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RFGanadosDia : MonoBehaviour {

	private int Game = 1;
	private Game game;
	private List<GameLevelDTO> levels;
	private List<GameObject> LevelCheckBox;

	void Start(){
		// Get game Data
		GameSQLite gamesql = new GameSQLite();
		game = gamesql.getGame(this.Game);

		// Get Levels of game
		GameLevelSQLite levelSQL = new GameLevelSQLite();
		levels = levelSQL.getAllLevelsOfGame(this.Game);

		//Add checkbox per level
		GameObject auxiliar;
		foreach(GameLevelDTO current in levels){
			auxiliar = Instantiate( Resources.Load("utils/Checkbox",typeof(GameObject))) as GameObject;
			auxiliar.GetComponentInChildren<Text>().text = current.Level.ToString();
			auxiliar.gameObject.transform.SetParent(this.gameObject.transform,false);
			LevelCheckBox.Add(auxiliar);
		}
	}

}
