using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserOptionalPanel : MonoBehaviour {

	private int UserId;
	private string Name;
	private string LastName;

	public GameObject NameInput;
	public GameObject LastNameInput;

	public void setUserInfo ( int UserId, string name, string lastName ){
		this.UserId = UserId;
		this.Name = name;
		this.LastName = lastName;
		updateNameInput();
	}

	private void updateNameInput (){
		if (this.Name != null){
			NameInput.GetComponent<InputField>().text = this.Name;
			NameInput.GetComponent<InputField>().ForceLabelUpdate();
		}
	}

	private void updateLastNameInput (){
		if (this.LastName != null){
			LastNameInput.GetComponent<InputField>().text = this.Name;
			LastNameInput.GetComponent<InputField>().ForceLabelUpdate();
		}
	}
}
