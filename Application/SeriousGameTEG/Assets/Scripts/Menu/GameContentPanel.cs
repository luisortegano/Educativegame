using UnityEngine;
using System.Collections;
using ORM;

public class GameContentPanel : MonoBehaviour {

	public GameObject UIManger;
	public GameObject gamePanelPrefab;

	void OnEnable() {
		GameObject[] ListGame = GameObject.FindGameObjectsWithTag("GamePanel");
		Debug.Log ("########## Game count= "+ListGame.Length);
		this.cleanGameContentPanel(ListGame);
		this.populateGame();
	}

	public void cleanGameContentPanel(GameObject[] ListGame){
		if(ListGame!=null && 0 < ListGame.Length ){
			foreach(GameObject current in ListGame){
				DestroyImmediate(current.gameObject);
			}
		}
	}

	public void populateGame(){
		GameSQLite gameORM = new GameSQLite ();
		if (gameORM.loadGames ()) {
			Debug.Log ("##########Games was loaded total: " + gameORM.Games.Rows.Count);
			foreach (DataRow game in gameORM.Games.Rows) {
				//this.instantiateGame ((string)game [GameSQLite.Name], (int)game [GameSQLite.Id_Category]);
				this.instantiateGame((string)game[GameSQLite.Name]);
			}
		} else {
			Debug.Log("##########Games was not loaded");
		}
	}

	public void instantiateGame (string Name) {
		GameObject newGamePanel = (GameObject)Instantiate(gamePanelPrefab); 
		GamePanel panel = newGamePanel.GetComponent<GamePanel>();
		panel.NameText.text = Name;
		newGamePanel.transform.SetParent(gameObject.transform);
		newGamePanel.transform.localScale = new Vector3(1,1,1);
	}

	public void instantiateGame (string Name, string Categoria) {
		GameObject newCategoryPanel = (GameObject)Instantiate(gamePanelPrefab); 
		GamePanel panel = newCategoryPanel.GetComponent<GamePanel>();
		panel.NameText.text = Name;
		panel.CategoriaText.text = Categoria;
		newCategoryPanel.transform.SetParent(gameObject.transform);
		newCategoryPanel.transform.localScale = new Vector3(1,1,1);
	}

	public void closeGameContentPanel (){
		UserInterfaceManager uim = UIManger.GetComponent<UserInterfaceManager>();
		if (uim != null) {
			uim.MenuSetActive(Menu.GameSelectionPanel,false);
			uim.MenuSetActive(Menu.HomePanel,true);
		}
	}
}
