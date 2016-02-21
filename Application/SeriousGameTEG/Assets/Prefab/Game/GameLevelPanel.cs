using UnityEngine;
using UnityEngine.UI;
using ORM;

public class GameLevelPanel : MonoBehaviour {
	public Text LevelText;
	public Text ConfigurationText;
	public Button PlayButton;
	
	public int CodeLevel;

	public void clickPlay(){
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		lr.insertLevelResult(um.getUserSelected(),CodeLevel);
		uim.getMenuGameobject(Menu.GameLevelContentPanel).SendMessage("refreshView");
	}
}
