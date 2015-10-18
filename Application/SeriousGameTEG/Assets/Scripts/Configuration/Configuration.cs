using UnityEngine;
using System.Collections;
using Persistence;

public class Configuration : MonoBehaviour {

	/*Interface GameObjects*/
	public GameObject LoadingGroup;


	private Persister<UserContainer> userPersister = null;
	private UserContainer Users = null;

	void Awake () {
		Debug.Log("Configuration is Awaking");

		if(this.Users == null){
			/*Load Users*/
			Users = this.UserPersister().load();
			Debug.Log("Objeto de usuarios cargado" + Users);
			Debug.Log("Usuarios Existentes: " + Users.Users.Count);
		}



		LoadingGroup.SetActive(false);
		Debug.Log("Configuration is Fully Load");
	}

	public Persister<UserContainer> UserPersister () {
		if (this.userPersister == null){ 
			this.userPersister = new Persister<UserContainer>(UserContainer.FILE_NAME); 
			this.userPersister.objToPersist = Users = new UserContainer();
		}
		return this.userPersister;
	}
}
