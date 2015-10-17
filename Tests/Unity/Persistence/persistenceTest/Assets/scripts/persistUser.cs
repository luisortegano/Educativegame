using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Persistence;

public class persistUser : MonoBehaviour {

	public Button saveButton;
	public Button loadButton;
	public Text nameText;
	public Text FNText;

	public Text FL; 
	private Persister<User> userPersist = null;

	private Persister<User> UserPersister (){
		if(this.userPersist == null){
			this.userPersist = new Persister<User>(User.LOCATION_PATH);
		}
		return this.userPersist;
	}

	public void saveUser (){

		Debug.Log("Save Button enabled: false");
		saveButton.enabled = false;

		Debug.Log("nameText: "+nameText.text);
		Debug.Log("FNText: "+FNText.text);

		User current = new User();
		current.Name = nameText.text;
		current.BirthDay = FNText.text;
		this.UserPersister().objToPersist = current;
		FL.text = this.UserPersister().dataFileName;
		this.UserPersister().save();

		Debug.Log("Save Button enabled: false");
		saveButton.enabled = true;
	}

	public void loadUser (){
		
		Debug.Log("Load Button enabled: false");
		loadButton.enabled = false;

		this.UserPersister().load();
		FL.text = this.UserPersister().objToPersist.Name + " " + this.UserPersister().objToPersist.BirthDay;

		Debug.Log("Load Button enabled: false");
		loadButton.enabled = true;
	}

}
