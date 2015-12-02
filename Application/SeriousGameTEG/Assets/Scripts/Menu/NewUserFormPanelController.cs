using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
		this.gameObject.SetActive(false);
		//insert into user (name, lastname) values ('no name','no last name');
		SqliteDatabase sql = new SqliteDatabase (Path.Combine (Application.persistentDataPath, "TEG_SG.db"));

		sql.ExecuteNonQuery("insert into user (name, lastname) values ('"+
		                    this.nameInputField.text.Trim()+"','"+
		                    this.lastNameInputField.text.Trim()+"');");
		if(this.isSuperUserToggle.isOn){

		}

		/*Guardar imagen en carpeta con el numero de id del usuario */


		this.UserSelectionPanel.SetActive(true);
	}

}
