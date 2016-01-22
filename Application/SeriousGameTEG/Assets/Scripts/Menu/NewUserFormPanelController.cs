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
	private string superUserPassword = "pass";

	public void CancelCreateNewUser () {
		this.gameObject.SetActive(false);
		this.UserSelectionPanel.SetActive(true);
	}

	public void CreateNewUser () {
		this.gameObject.SetActive(false); //deactivate button


		//If want to register like super user
		if(this.isSuperUserToggle.isOn && !superUserPassword.Equals(supeUserInputField.text)){
			AndroidNativePopups.OpenAlertDialog(
				"Clave Incorrecta", "La clave de super usuario es incorrecta.",
				"Continuar", "Cancelar", 
				() => {
				UserSQLite user = new UserSQLite();
				int idOfUser = user.SaveUser(this.nameInputField.text.Trim(),this.lastNameInputField.text.Trim());

				/*Guardar imagen en carpeta con el numero de id del usuario */
				if( !Directory.Exists(UserSQLite.PROFILE_IMAGE_PATH) ){
					Directory.CreateDirectory(UserSQLite.PROFILE_IMAGE_PATH);
				}

				File.Move(CameraController.temporalCapture,UserSQLite.PROFILE_IMAGE_PATH+Path.PathSeparator+idOfUser+".png");
				
				this.UserSelectionPanel.SetActive(true);
			},
			() => {
				this.gameObject.SetActive(true); //deactivate button
			});
		}else{
			AndroidNativePopups.OpenProgressDialog("Espere","Guardando usuario");
			UserSQLite user = new UserSQLite();
			int idOfUser = user.SaveUser(this.nameInputField.text.Trim(),this.lastNameInputField.text.Trim());
			
			/*Guardar imagen en carpeta con el numero de id del usuario */
			if( !Directory.Exists(UserSQLite.PROFILE_IMAGE_PATH) ){
				Directory.CreateDirectory(UserSQLite.PROFILE_IMAGE_PATH);
			}
			File.Move(CameraController.temporalCapture,UserSQLite.PROFILE_IMAGE_PATH+Path.PathSeparator+idOfUser+".png");

			this.UserSelectionPanel.SetActive(true);
			AndroidNativePopups.CloseProgressDialog();
		}
	}
}
