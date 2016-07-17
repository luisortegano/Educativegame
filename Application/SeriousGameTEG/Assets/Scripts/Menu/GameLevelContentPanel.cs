using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ORM;

public class GameLevelContentPanel : MonoBehaviour {

	public GameObject UIManger;
	public GameObject gameLevelPanelPrefab;
	private int GameIdSelected = 0;

	public void setGameIdSelected (int SelectedGameId){
		Debug.Log("@setGameIdSelected selectedGameId: "+ SelectedGameId);
		this.GameIdSelected = SelectedGameId;
		this.OnEnable();
	}

	void OnEnable() {
		Debug.Log("GameLevelContentPanes is enable");
		ScrollRect scroll = this.gameObject.GetComponentInParent<ScrollRect> ();
		scroll.content = this.gameObject.GetComponent<RectTransform> ();
		GameObject[] ListGame = GameObject.FindGameObjectsWithTag("GameLevelPanel");
		this.cleanGameLevelContentPanel(ListGame);
		this.populateGameLevel();
		scroll.verticalNormalizedPosition = 1f; // for positionate at the first element
	}

	public void cleanGameLevelContentPanel(GameObject[] ListGameLevel){
		if(ListGameLevel!=null && 0 < ListGameLevel.Length ){
			foreach(GameObject current in ListGameLevel){
				DestroyImmediate(current.gameObject);
			}
		}
	}

	public void populateGameLevel(){
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		GameLevelSQLite gameORM = new GameLevelSQLite ();
		Debug.Log("### UserSelected="+um.getUserSelected()+"| GameIDSelected="+this.GameIdSelected);
		foreach (GameLevelDTO level in gameORM.getLevelsOfGame ( um.getUserSelected(), this.GameIdSelected)) {
			this.instantiateGameLevel(this.GameIdSelected, level.Code, level.Level, level.Configuration);
		}
	}
	
	public void instantiateGameLevel (int GameId, int LevelCode, int Level, string Configuration) {
		GameObject newGameLevel = (GameObject)Instantiate(gameLevelPanelPrefab); 
		GameLevelPanel levelGamePanel = newGameLevel.GetComponent<GameLevelPanel>();
		levelGamePanel.CodeLevel = LevelCode;
		levelGamePanel.LevelText.text = ("Level: " + Level.ToString());
		levelGamePanel.ConfigurationText.text = Configuration;
		levelGamePanel.GameId = GameId;
		newGameLevel.transform.SetParent(gameObject.transform);
		newGameLevel.transform.localScale = new Vector3(1,1,1);

	}
	
	public void closeGameLevelContentPanel (){
		UserInterfaceManager uim = UIManger.GetComponent<UserInterfaceManager>();
		if (uim != null) {
			uim.MenuSetActive(Menu.GameLevelContentPanel,false);
			uim.MenuSetActive(Menu.GameContentPanel,true);
		}
	}

	public void backButtonCallback (){
		closeGameLevelContentPanel ();
	}
	
	public void refreshView (){
		this.OnEnable();
	}
}
