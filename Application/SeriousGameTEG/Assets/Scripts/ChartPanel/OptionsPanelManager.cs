using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ORM;

/* TAG = OptionChartManager */

public class OptionsPanelManager : MonoBehaviour {

	public Dropdown dropDownGames;
	public Dropdown dropDownLevels;

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

	void populateGamesDropDownMenu (){
		if (getGames().loadAllGames()){
			List<string> list = new List<string>();
			foreach(DataRow dr in this.getGames().Games.Rows ){
				list.Add((string)dr[GameSQLite.Name]);
			}
			dropDownGames.ClearOptions();
			dropDownGames.AddOptions(list);
		}
	}

	void populateGameLevelDropDownMenu (int IdGame){
		List<string> list = new List<string>();
		foreach( DataRow dr in this.getLevels().getAllLevelsOfGame(IdGame).Rows ){
			list.Add(((int)dr[GameLevelSQLite.Level]).ToString());
		}
		dropDownLevels.ClearOptions();
		dropDownLevels.AddOptions(list);
	}

	public void selectGameFromDropDown(int selectedIndex){
		DataRow dr = this.getGames().Games.Rows[selectedIndex];
		Debug.Log("### Game was selected" + (string)dr[GameSQLite.Name] + " id="+(int)dr[GameSQLite.Id]);
		populateGameLevelDropDownMenu((int)dr[GameSQLite.Id]);
	}

	public void setUserId (int UserId){
		
	}

	void OnEnable (){
		this.populateGamesDropDownMenu();
		selectGameFromDropDown(this.dropDownGames.value);

		if(this.selectUserId <= 0){
			GameObject[] thumbs = GameObject.FindGameObjectsWithTag("UserThumbButton");
			if(thumbs!=null){
				this.selectUserId = thumbs[0].GetComponent<UserChartThumbButton>().userId;
				Debug.Log("####Setting selected User =" + this.selectUserId);
			}
		}
	}

}
