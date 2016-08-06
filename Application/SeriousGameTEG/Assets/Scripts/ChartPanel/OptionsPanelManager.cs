using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ORM;

/* TAG = OptionChartManager */

[Obsolete]
public class OptionsPanelManager : MonoBehaviour {

	public GameObject viewChartPanel;

	public int selectUserId;

	private GameSQLite games;
	private GameLevelSQLite levels;
	private QueryUtils qutils;

	GameSQLite getGames() {
		if(this.games==null) this.games = new GameSQLite ();
		return this.games;
	}

	GameLevelSQLite getLevels() {
		if(this.levels==null) this.levels = new GameLevelSQLite ();
		return this.levels;
	}

	QueryUtils getQueryUtils (){
		if(this.qutils==null)
			this.qutils = new QueryUtils ();
		return this.qutils;
	}

	public void setUserId (int UserId){
		this.selectUserId = UserId;
	}

	public int getCurrentUser(){
		return this.selectUserId;
	}
}
