using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ORM;

public class GameLevelContentPanel : MonoBehaviour {

	public GameObject UIManger;
	public GameObject gameLevelPanelPrefab;
	
	void OnEnable() {
		this.gameObject.GetComponentInParent<ScrollRect> ().content = this.gameObject.GetComponent<RectTransform> ();
		GameObject[] ListGame = GameObject.FindGameObjectsWithTag("GameLevelPanel");
		this.cleanGameLevelContentPanel(ListGame);
		this.populateGameLevel();
	}

	public void cleanGameLevelContentPanel(GameObject[] ListGameLevel){
		if(ListGameLevel!=null && 0 < ListGameLevel.Length ){
			foreach(GameObject current in ListGameLevel){
				DestroyImmediate(current.gameObject);
			}
		}
	}

	public void populateGameLevel(/*int IdGame*/){
		int IdGame = 1;
		GameLevelSQLite gameORM = new GameLevelSQLite ();
		DataTable dt = gameORM.getLevelsOfGame (IdGame);
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
			uim.MenuSetActive(Menu.GameSelectionPanel,false);
			uim.MenuSetActive(Menu.HomePanel,true);
		}
	}
}
