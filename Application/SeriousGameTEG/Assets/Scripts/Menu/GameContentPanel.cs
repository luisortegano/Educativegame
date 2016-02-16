using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ORM;

public class GameContentPanel : MonoBehaviour {

	public GameObject UIManger;
	public GameObject gamePanelPrefab;

	void OnEnable() {
		this.gameObject.GetComponentInParent<ScrollRect> ().content = this.gameObject.GetComponent<RectTransform> ();
		GameObject[] ListGame = GameObject.FindGameObjectsWithTag("GamePanel");
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
			foreach (DataRow game in gameORM.Games.Rows) {
				this.instantiateGame((int)game[GameSQLite.Id], (string)game[GameSQLite.Name], (string)game["category_name"]);
			}
		}
	}

	public void instantiateGame (int GameId, string Name, string Categoria) {
		GameObject newCategoryPanel = (GameObject)Instantiate(gamePanelPrefab); 
		GamePanel panel = newCategoryPanel.GetComponent<GamePanel>();
		panel.GameId = GameId;
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

	public void backButtonCallback (){
		closeGameContentPanel ();
	}
}
