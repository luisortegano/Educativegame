using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/* Esta clase contiene los elementos mas importantes de la UI y sirve
 como controlador para que interceda en acciones muy usadas o enlazando el objeto necesario

 es importante cuidar el orden del enum con los elementos que se pasan al MenuList*/

public enum Menu {
	Canvas=0, 
	HubPanel, 
	HomePanel, 
	UserSelectionPanel, 
	NewUserFormPanel, 
	GameSelectionPanel,
	GameContentPanel,
	GameLevelContentPanel
};

public class UserInterfaceManager : MonoBehaviour {

	/*Menu*/
	public List<GameObject> MenuList;

	public void MenuSetActive ( Menu i, bool active ){
		MenuList[(int)i].SetActive(active);
	}

	public bool MenuActiveSelf (Menu i){
		return MenuList[(int)i].activeSelf;
	}

	public GameObject getMenuGameobject(Menu obj){
		return MenuList[(int)obj];
	}

	public void DisplayHome (){
		MenuList[(int)Menu.HubPanel].SetActive(true);
		MenuList[(int)Menu.HomePanel].SetActive(true);
		MenuList[(int)Menu.UserSelectionPanel].SetActive(false);
		MenuList[(int)Menu.NewUserFormPanel].SetActive(false);
		MenuList[(int)Menu.GameSelectionPanel].SetActive(false);
	}
}
