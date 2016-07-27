using ORM;
using GenericPerDayChart;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GenericPerDay : MonoBehaviour, Report {

	int UserId;

	public void setUserId (int UserId){
		this.UserId = UserId;
	}

	public GameObject filterPanelObjects;

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

		//Map results per date
		SortedDictionary<DateTime,List<LevelResult>>  mappedResults = new SortedDictionary<DateTime, List<LevelResult>>();
		foreach(LevelResult current in levelResults){
			DateTime date = Convert.ToDateTime(current.Result.date);

			if( mappedResults.ContainsKey(date.Date) ){
				mappedResults[date.Date].Add(current);
			}else{
				List<LevelResult> newList = new List<LevelResult>();
				newList.Add(current);
				mappedResults.Add(date.Date,newList);
			}
		}

		DataGenericPerDay dataGeneric = new DataGenericPerDay();
		foreach(KeyValuePair<DateTime,List<LevelResult>> listResultsPerDay in mappedResults){
			DayChart daychart = new DayChart("valued",listResultsPerDay.Key.ToString());
			Value wins = new Value();
			wins.label="win";
			wins.color="#00f";

			foreach( LevelResult current in listResultsPerDay.Value )
				if(current.Win) wins.amount++;

			if(0<wins.amount)
				daychart.addValue(wins);

			if(listResultsPerDay.Value.Count != wins.amount){
				daychart.addValue(new Value("LOSE",listResultsPerDay.Value.Count-wins.amount,"#f00"));
			}

			dataGeneric.addDayChart(daychart);
		}

		Debug.Log("#####dataPIE" + JsonUtility.ToJson(dataGeneric));
		System.IO.File.WriteAllText( System.IO.Path.Combine(Application.persistentDataPath, "pieDataPerDay.json"),
			JsonUtility.ToJson(dataGeneric,true) );

		StartCoroutine(copyHtmlToIndex());

	}

	IEnumerator copyHtmlToIndex(){
		Debug.Log("##### Starting Copy of PieChart.html");
		var src = System.IO.Path.Combine(Application.streamingAssetsPath, "PieChart.html");
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
        BroadcastMessage("loadURL","index.html");
		Debug.Log("##### Finishing Copy of PieChart.html");
        yield break;
	}

}
