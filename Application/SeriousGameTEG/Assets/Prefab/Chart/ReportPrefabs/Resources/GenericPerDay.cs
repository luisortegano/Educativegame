using ORM;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GenericPerDay : MonoBehaviour {

	public GameObject filterPanelObjects;

	private GameSQLite gameDB;
	private GameSQLite gameDatabase(){
		if(gameDB==null)
			gameDB = new GameSQLite();
		return gameDB;
	}

	//Report's Option
	List<Game> games;
	GameObject gamesDropdown;

	void Awake(){
		//Find games
		games = gameDatabase().getAllGames();
	}

	// Use this for initialization
	void Start () {

		//Set games to game's Dropdown
		gamesDropdown = Instantiate( Resources.Load("utils/Dropdown",typeof(GameObject))) as GameObject;
		Dropdown gameDropdownUI = gamesDropdown.GetComponent<Dropdown>();
		gameDropdownUI.options.Clear();
		foreach(Game current in games){
			gameDropdownUI.options.Add(new Dropdown.OptionData(current.Name));
		}
		gameDropdownUI.gameObject.transform.SetParent(filterPanelObjects.gameObject.transform,false);
		gameDropdownUI.RefreshShownValue();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
