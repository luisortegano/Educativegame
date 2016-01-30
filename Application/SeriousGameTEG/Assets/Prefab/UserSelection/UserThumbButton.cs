using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserThumbButton : MonoBehaviour {
	public Button button;
	public Text nameLabel;
	public Text lastNameLabel;
	public int userId;

	public void setAsActualuser (){
		GameObject ConfigurationObject = GameObject.FindGameObjectWithTag("ConfigurationObject");
		ConfigurationObject.SendMessage("setUserWithUserId", this.userId);
		GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
		UIManager.SendMessage("DisplayHome");
	}
}
