using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TheNextFlow.UnityPlugins;
using System.IO;

public class NewUserFormPanelController : MonoBehaviour {

	public GameObject UserSelectionPanel;

	public InputField nameInputField;
	public InputField lastNameInputField;
	public InputField supeUserInputField;
	public Toggle isSuperUserToggle;

	public void CancelCreateNewUser () {
		this.gameObject.SetActive(false);
		this.UserSelectionPanel.SetActive(true);
	}

	public void CreateNewUser () {
		this.gameObject.SetActive(false); //deactivate button
		AndroidNativePopups.OpenProgressDialog("Espere","Guardando usuario");
		
		if(this.isSuperUserToggle.isOn){
			string password = "pass";
			if (!password.Equals(supeUserInputField.text)){
				AndroidNativePopups.OpenAlertDialog(
					"Clave Incorrecta", "La clave de super usuario es incorrecta.",
					"Continuar", 
					() => {

					UserSQLite user = new UserSQLite();
					int idOfUser = user.SaveUser(this.nameInputField.text.Trim(),this.lastNameInputField.text.Trim());

					/*Guardar imagen en carpeta con el numero de id del usuario */
					
					this.UserSelectionPanel.SetActive(true);
					AndroidNativePopups.CloseProgressDialog();
					Debug.Log("Accept was pressed"); 
				});
			}
		}else{
			UserSQLite user = new UserSQLite();
			int idOfUser = user.SaveUser(this.nameInputField.text.Trim(),this.lastNameInputField.text.Trim());

			/*Guardar imagen en carpeta con el numero de id del usuario */
			
			this.UserSelectionPanel.SetActive(true);
			AndroidNativePopups.CloseProgressDialog();
		}
	}
}
