using UnityEngine;
using UnityEngine.UI;
using ORM;

public class GameLevelPanel : MonoBehaviour {
	public Text LevelText;
	public Text ConfigurationText;
	public Button PlayButton;
	
	public int GameId;
	public int CodeLevel;

	public void clickPlay(){
		Debug.Log ("Loading Game ("+GameId+")");
		
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		uim.MenuSetActive(Menu.Canvas,false);
		Application.LoadLevelAdditive(GameId);
		
		
		/*
		//Demostration of avance in levels
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		lr.insertLevelResult(um.getUserSelected(),CodeLevel);
		uim.getMenuGameobject(Menu.GameLevelContentPanel).SendMessage("refreshView");
		*/
	}
}
