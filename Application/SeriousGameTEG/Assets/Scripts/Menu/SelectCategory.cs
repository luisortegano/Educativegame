using UnityEngine;
using System.Collections;
using TheNextFlow.UnityPlugins;
using System.Collections.Generic;

public class SelectCategory : MonoBehaviour {

	/*@Canvas>HomePanel>CentralButtonPanel*/
	public GameObject configurationGameObject;

	public void clickCategoryButton() {
		this.gameObject.SetActive(false);
		UserManager userManagerConfiguration = configurationGameObject.GetComponent<UserManager>();

		if(userManagerConfiguration == null ){
			AndroidNativePopups.OpenAlertDialog("Ups!","Al parecer No existe componente de UserManager.",
			                                    "continuar",
			                                    () => {
				this.gameObject.SetActive(true);
			});
			Debug.LogError("No existe componente de UserManager");
		}

		if( !userManagerConfiguration.isUserSelected() ){
			AndroidNativePopups.OpenAlertDialog("Wooot!", "Debes Seleccionar un jugador primero!", "Continuar",
                () => {
				this.gameObject.SetActive(true);
			});
			Debug.Log("No se ha seleccionado ningun usuario");
			return;
		}

	}
}
