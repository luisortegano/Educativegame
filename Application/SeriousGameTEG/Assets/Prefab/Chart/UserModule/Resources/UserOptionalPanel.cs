using UnityEngine;
using UnityEngine.UI;
using ORM;
using System.Collections;

public class UserOptionalPanel : MonoBehaviour {

	private User user;

	public GameObject NameInput;
	public GameObject LastNameInput;

	public void setUserInfo (User user){
		this.user = user;
		updateNameInput();
		updateLastNameInput();
	}

	private void updateNameInput (){
		if (this.user.Name != null){
			NameInput.GetComponent<InputField>().text = this.user.Name;
			NameInput.GetComponent<InputField>().ForceLabelUpdate();
		}
	}

	private void updateLastNameInput (){
		if (this.user.LastName != null){
			LastNameInput.GetComponent<InputField>().text = this.user.LastName;
			LastNameInput.GetComponent<InputField>().ForceLabelUpdate();
		}
	}

	public void clearInputs (){
		NameInput.GetComponent<InputField>().text = "";
		LastNameInput.GetComponent<InputField>().text = "";
		NameInput.GetComponent<InputField>().ForceLabelUpdate();
		LastNameInput.GetComponent<InputField>().ForceLabelUpdate();
	}

	public void updateUser (){
		UserSQLite userSQL = new UserSQLite ();
		this.user.Name = this.NameInput.GetComponent<InputField>().text;
		this.user.LastName = this.LastNameInput.GetComponent<InputField>().text;
		userSQL.updateUserData(this.user);
	}
}
