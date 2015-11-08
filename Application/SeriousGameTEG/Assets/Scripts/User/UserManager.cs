using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Persistence;

public class UserManager : MonoBehaviour {

	/*UserData on view*/
	public InputField nameInputField;
	public InputField lastNameInputField;

	private Persister persisterXML;

	/*Collection of users on system*/
	private UserContainer userContainer = null;
	/*User selected in menu*/
	private User userSelected = null;

	/*When UserManagers awake load the users*/
	void Awake (){
		/*Make it Permanent*/
		DontDestroyOnLoad(gameObject);

		/*Load users*/
		if(this.userContainer == null){
			userContainer = this.PersisterXML().Load<UserContainer>(UserContainer.FILE_NAME );
			/*No existe aun un archivo de usuarios*/
			if(userContainer == null){
				userContainer = new UserContainer ();
				this.PersisterXML().Save<UserContainer>(UserContainer.FILE_NAME ,userContainer);
				Debug.Log("Se creo el archivo "+UserContainer.FILE_NAME+" sin data.");
			}
		}
	}

	public void createUser(){
		if( nameInputField.text.Trim().Length != 0 && lastNameInputField.text.Trim().Length != 0 ){
			if(userContainer != null){
				User newUser = new User ();
				newUser.Name = nameInputField.text.Trim();
				newUser.LastName = lastNameInputField.text.Trim();
				newUser.UserId = userContainer.Users.Count;
				userContainer.Users.Add(newUser);
				this.PersisterXML().Save<UserContainer>(UserContainer.FILE_NAME,userContainer);
				nameInputField.text = "";
				lastNameInputField.text = "";
				Debug.Log(userContainer.Users.Count);
			}else{
				Debug.LogError("userContainer es null, ya deberia estar instanciado.");
			}
		}
	}

	public void setUserWithUserId (int UserId){
		if (userContainer == null ) return;
		this.userSelected = userContainer.Users.Find(f => f.UserId == UserId);
		Debug.Log(this.userSelected.Name);
		Debug.Log(this.userSelected.LastName);
		Debug.Log(this.userSelected.UserId);
	}

	public List<User> getUserList (){
		if (userContainer != null)
			return userContainer.Users;
		return new List<User>();
	}

	public Persister PersisterXML () {
		if (this.persisterXML == null) this.persisterXML = new Persister();
		return this.persisterXML;
	}
}
