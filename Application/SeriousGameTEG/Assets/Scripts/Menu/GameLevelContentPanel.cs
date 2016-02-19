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
		GameLevelSQLite gameORM = new GameLevelSQLite ();
		DataTable dt = gameORM.getLevelsOfGame (this.GameIdSelected);
		foreach (DataRow level in dt.Rows) {
			this.instantiateGameLevel((int)level[GameLevelSQLite.Level],(string)level[GameLevelSQLite.Configuration]);
		}
	}
	
	public void instantiateGameLevel (int Level, string Configuration) {
		GameObject newGameLevel = (GameObject)Instantiate(gameLevelPanelPrefab); 
		GameLevelPanel panel = newGameLevel.GetComponent<GameLevelPanel>();
		panel.LevelText.text = ("Level: " + Level.ToString());
		panel.ConfigurationText.text = Configuration;
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
}
