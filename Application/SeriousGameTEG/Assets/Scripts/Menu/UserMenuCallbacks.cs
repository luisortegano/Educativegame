using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Persistence;


public class UserMenuCallbacks : MonoBehaviour {

	private Persister persister;

	public GameObject UserScrollPanel;
	public GameObject NewUserPanel;

	private Hashtable menuObjects; /*Contains de object for all User Menu*/

	public void Awake (){
		/*Collect de instances of de menus*/
		menuObjects = new Hashtable();
	}

	public void OnClickCreateNewUser () {
		Debug.Log ("OnClickCreateNewUser has triggered");
		/*Ocultar elementos en el panel que no tengan que ver con creacion de usuario */
		UserScrollPanel.SetActive(false);
		NewUserPanel.SetActive(true);
	}

	public void OnClickCreateUser(){
		Debug.Log ("OnClickCreateUser has triggered");
		/*Ocultar elementos en el panel que no tengan que ver con creacion de usuario */
		NewUserPanel.SetActive(false);
		UserScrollPanel.SetActive(true);
	}

	private void SetActiveAllByTag(bool active, string TAG ){
		//Debug.Log(GameObject.FindGameObjectsWithTag(tag).Length);
		foreach( GameObject current in (GameObject[])menuObjects[TAG] ){
			current.gameObject.SetActive(active);
		}
	}

	public Persister PersisterTool (){
		if (persister == null){
			persister = new Persister();
		}
		return persister;
	}

}
