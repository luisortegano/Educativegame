using UnityEngine;
using System.Collections;

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
		for ( int i = 0 ; i < 4 ; i++ ){
			this.instantiateGame();
		}
	}

	public void instantiateGame () {
		GameObject newCategoryPanel = (GameObject)Instantiate(gamePanelPrefab); 
		GamePanel panel = newCategoryPanel.GetComponent<GamePanel>();
		panel.testText.text = "prueba";
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
