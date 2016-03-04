using UnityEngine;
using System.Collections;

public class SelectChart : MonoBehaviour {

	public GameObject uiManager;

	public void clickChart (){
		UserInterfaceManager uim = uiManager.GetComponent<UserInterfaceManager>();
		uim.MenuSetActive(Menu.HomePanel,false);
		uim.MenuSetActive(Menu.HubPanel,false);
		uim.MenuSetActive(Menu.ChartPanel,true);
	}
}
