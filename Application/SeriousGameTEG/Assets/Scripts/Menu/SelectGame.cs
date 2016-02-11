using UnityEngine;
using System.Collections;
using TheNextFlow.UnityPlugins;
using System.Collections.Generic;

public class SelectGame : MonoBehaviour {

	/*@Canvas>HomePanel>CentralButtonPanel*/
	public GameObject configurationGameObject;
	public GameObject UIManager;

	public void clickGameButton() {
		UserInterfaceManager uim = UIManager.GetComponent<UserInterfaceManager> ();
		UserManager userManagerConfiguration = configurationGameObject.GetComponent<UserManager>();

		if (uim == null || userManagerConfiguration == null) {
			Debug.Log("Error @SelectGame class");
			return;
		}
		uim.MenuSetActive (Menu.HomePanel, false);

		if(userManagerConfiguration == null ){
			AndroidNativePopups.OpenAlertDialog("Ups!","Al parecer No existe componente de UserManager.",
			                                    "continuar",
			                                    () => {
				uim.MenuSetActive (Menu.HomePanel, true);
			});
			Debug.LogError("No existe componente de UserManager");
		}

		if( !userManagerConfiguration.isUserSelected() ){
			AndroidNativePopups.OpenAlertDialog("Wooot!", "Debes Seleccionar un jugador primero!", "Continuar",
                () => {
				uim.MenuSetActive (Menu.HomePanel, true);
			});
			Debug.Log("No se ha seleccionado ningun usuario");
			return;
		}else{
			//hide this
			uim.MenuSetActive (Menu.HomePanel, false);
			uim.MenuSetActive (Menu.GameSelectionPanel, true);
		}
	}
}
