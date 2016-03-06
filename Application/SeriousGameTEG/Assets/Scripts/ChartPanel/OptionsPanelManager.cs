using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ORM;

public class OptionsPanelManager : MonoBehaviour {

	public Dropdown dropDownGames;

	private GameSQLite games;
	private QueryUtils qutils;

	GameSQLite getGames() {
		if(this.games==null)
			this.games = new GameSQLite ();
		return this.games;
	}

	QueryUtils getQueryUtils (){
		if(this.qutils==null)
			this.qutils = new QueryUtils ();
		return this.qutils;
	}

	void populateGamesDropDownMenu (){
		if (getGames().loadAllGames()){
			List<string> list = new List<string>();
			foreach(DataRow dr in this.getGames().Games.Rows ){
				list.Add((string)dr[GameSQLite.Name]);
			}
			dropDownGames.AddOptions(list);
		}
	}

	void OnEnable (){
		this.populateGamesDropDownMenu();
	}

}
