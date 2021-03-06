﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ORM;

public class GameContentPanel : MonoBehaviour {

	public GameObject UIManger;
	public GameObject gamePanelPrefab;

	void OnEnable() {
		ScrollRect scroll = this.gameObject.GetComponentInParent<ScrollRect> ();
		scroll.content = this.gameObject.GetComponent<RectTransform> ();
		GameObject[] ListGame = GameObject.FindGameObjectsWithTag("GamePanel");
		this.cleanGameContentPanel(ListGame);
		this.populateGame();
		scroll.verticalNormalizedPosition = 1f; // for positionate at the first element
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
		UserManager userManager = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();

		if (gameORM.loadGames (userManager.getUserSelected())) {
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
		panel.setImage();
		newCategoryPanel.transform.SetParent(gameObject.transform,false);
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
