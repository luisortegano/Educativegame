using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using ORM;

public class UserThumbButton : MonoBehaviour {
	public Button button;
	public int userId;

	public RawImage avatarUser;

	public void setAsActualuser (){
		GameObject ConfigurationObject = GameObject.FindGameObjectWithTag("ConfigurationObject");
		ConfigurationObject.SendMessage("setUserWithUserId", this.userId);

		GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
		UIManager.SendMessage("DisplayHome");
	}
}
