using UnityEngine;
using System.Collections.Generic;
using Persistence;

public class Configuration : MonoBehaviour {

	/*Interface GameObjects*/
	public GameObject LoadingGroup;


	private Persister persisterXML = null;
	private UserContainer Users = null;

	void Awake () {
		Debug.Log("Configuration is Awaking");

		if(this.Users == null){

			Users = this.PersisterXML().Load<UserContainer>(UserContainer.FILE_NAME );
			/*No existe aun un archivo de usuarios*/
			if(Users == null){
				Users = new UserContainer ();
				this.PersisterXML().Save<UserContainer>(UserContainer.FILE_NAME ,Users);
				Debug.Log("Se creo el archivo "+UserContainer.FILE_NAME+" sin data.");
			}

			Debug.Log("Objeto de usuarios cargado" + Users);
			Debug.Log("Usuarios Existentes: " + Users.Users.Count);
		}



		LoadingGroup.SetActive(false);
		Debug.Log("Configuration is Fully Load");
	}

	public Persister PersisterXML () {
		if (this.persisterXML == null){ 
			this.persisterXML = new Persister(); 
		}
		return this.persisterXML;
	}
}
