using UnityEngine;
using System.Collections;

public class GameSelectionPanel : MonoBehaviour {

	public GameObject UIManager;

	public void clickBackbutton(){
		UserInterfaceManager uim = UIManager.GetComponent<UserInterfaceManager> ();
		uim.getMenuGameobject (Menu.GameContentPanel).SendMessage("backButtonCallback");
		uim.getMenuGameobject (Menu.GameLevelContentPanel).SendMessage("backButtonCallback");
	}
}
