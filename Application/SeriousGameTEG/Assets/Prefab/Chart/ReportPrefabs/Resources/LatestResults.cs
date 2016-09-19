using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System.Collections;
using System;
using ORM;
using LastestResults;

public class LatestResults : MonoBehaviour, Report {

	int UserId;
	private string serial;

	//Prefab links
	public GameObject filterPanelObjects;

	public void generateSerial (){
		serial = "lr"+ DateTime.Now.ToString("yyyyMMddHHmmss");
	}

	public void setUserId (int UserId){
		this.UserId = UserId;
	}

	private GameSQLite gameDB;
	private GameSQLite gameDatabase(){
		if(gameDB==null) gameDB = new GameSQLite();
		return gameDB;
	}

	private GameLevelSQLite levelDB;
	private GameLevelSQLite levelDatabase(){
		if(levelDB==null) levelDB = new GameLevelSQLite();
		return levelDB;
	}

	private LevelResultsSQLite resultsDB;
	private LevelResultsSQLite resultsDatabase(){
		if(resultsDB==null) resultsDB = new LevelResultsSQLite();
		return resultsDB;
	}

	//Report's Option
	List<Game> games;
	GameObject gamesDropdown;

	List<GameLevelDTO> levels;
	GameObject levelsDropdown;

	void Awake(){
		//Set games to game's Dropdown
		games = this.gameDatabase().getAllGames();
	}

	// Use this for initialization
	void Start () {
		
		//Game label
		GameObject textGameGO = Instantiate( Resources.Load("utils/Text",typeof(GameObject))) as GameObject;
		Text textGames = textGameGO.GetComponent<Text>();
		textGames.text="Juego";
		textGameGO.transform.SetParent(filterPanelObjects.gameObject.transform,false);
		//Game dropdown
		gamesDropdown = Instantiate( Resources.Load("utils/Dropdown",typeof(GameObject))) as GameObject;
		Dropdown gameDropdownUI = gamesDropdown.GetComponent<Dropdown>();
		gameDropdownUI.options.Clear();
		foreach(Game current in games){
			gameDropdownUI.options.Add(new Dropdown.OptionData(current.Name));
		}
		gamesDropdown.gameObject.transform.SetParent(filterPanelObjects.gameObject.transform,false);
		gameDropdownUI.RefreshShownValue();

		//Levels dropdown
		GameObject textLevelGO = Instantiate( Resources.Load("utils/Text",typeof(GameObject))) as GameObject;
		Text textLevelGames = textLevelGO.GetComponent<Text>();
		textLevelGames.text="Nivel";
		textLevelGO.transform.SetParent(filterPanelObjects.gameObject.transform,false);
		Debug.Log("##### level dropdown GPD");
		levels = levelDatabase().getAllLevelsOfGame(games[gameDropdownUI.value].Id);
		levelsDropdown = Instantiate( Resources.Load("utils/Dropdown",typeof(GameObject))) as GameObject;
		Dropdown levelsDropdownUI = levelsDropdown.GetComponent<Dropdown>();
		levelsDropdownUI.options.Clear();
		foreach(GameLevelDTO current in levels){
			levelsDropdownUI.options.Add(new Dropdown.OptionData(current.Level.ToString()));
		}
		levelsDropdown.gameObject.transform.SetParent(filterPanelObjects.gameObject.transform,false);
		levelsDropdownUI.RefreshShownValue();

		levelsDropdownUI.onValueChanged.AddListener(delegate {
			getActualResults();
		});

		getActualResults();
	}

	public void getActualResults(){
		Dropdown gameDropdownUI = gamesDropdown.GetComponent<Dropdown>();
		Dropdown levelsDropdownUI = levelsDropdown.GetComponent<Dropdown>();

		List<LevelResult> levelResults = this.resultsDatabase().getLevelResults(this.UserId, games[gameDropdownUI.value].Id, levels[levelsDropdownUI.value].Level);
//		List<LevelResult> levelResults = this.resultsDatabase().getLevelResults(this.UserId, 1, 2);

		//Map results per date
		SortedDictionary<DateTime,List<LevelResultJson>>  mappedResults = new SortedDictionary<DateTime, List<LevelResultJson>>();
		foreach(LevelResult current in levelResults){
			DateTime date = Convert.ToDateTime(current.Result.date);
			if( mappedResults.ContainsKey(date.Date) ){
				mappedResults[date.Date].Add(current.Result);
			}else{
				List<LevelResultJson> newList = new List<LevelResultJson>();
				newList.Add(current.Result);
				mappedResults.Add(date.Date,newList);
			}
		}

		DataLatestResults dataLatest = new DataLatestResults (mappedResults);
		this.generateSerial();
		System.IO.File.WriteAllText( System.IO.Path.Combine(Application.persistentDataPath, serial+".json"),
			JsonUtility.ToJson(dataLatest,true) );

		Debug.Log(string.Format("###### LatestResultJson Generated game[{0}]level[{1}] json[{2}]",
			games[gameDropdownUI.value].Id, levels[levelsDropdownUI.value].Level, JsonUtility.ToJson(dataLatest)));
		
		StartCoroutine(((Report)this).copyHtmlToIndex());
	}

	IEnumerator Report.copyHtmlToIndex(){
		Debug.Log("##### Starting Copy of BarChart.html");
		var src = System.IO.Path.Combine(Application.streamingAssetsPath, "BarChart.html");
		var dst = System.IO.Path.Combine(Application.persistentDataPath, "index.html");
        byte[] result = null;
        if (src.Contains("://")) {
            var www = new WWW(src);
            yield return www;
            result = www.bytes;
        } else {
            result = System.IO.File.ReadAllBytes(src);
        }
		System.IO.File.WriteAllBytes(dst, result);

		Debug.Log(string.Format("##### Finishing Copy of [{0}]",dst));
		Debug.Log(string.Format("##### Loading [{0}]","BarChart.html?data="+serial));
		GameObject.FindGameObjectWithTag("ChartPanelRoot").SendMessage("loadURL","index.html?data="+serial);
	}
}
