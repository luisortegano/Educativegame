using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Persistence;


public class UserMenuCallbacks : MonoBehaviour {

	/*Elements Tags for UI identification*/
	public static string TAG_USER_MENU_MAIN = "UserMenuMain";
	public static string TAG_CREATE_NEW_USER_FORM = "CreateNewUserForm";

	private Hashtable menuObjects; /*Contains de object for all User Menu*/


	public void Awake (){
		/*Collect de instances of de menus*/
		menuObjects = new Hashtable();
		menuObjects.Add(TAG_USER_MENU_MAIN,GameObject.FindGameObjectsWithTag(TAG_USER_MENU_MAIN));
		menuObjects.Add(TAG_CREATE_NEW_USER_FORM,GameObject.FindGameObjectsWithTag(TAG_CREATE_NEW_USER_FORM));
		SetActiveAllByTag(false, TAG_CREATE_NEW_USER_FORM);
	}

	public void OnClickCreateNewUser () {
		Debug.Log ("OnClickCreateNewUser has triggered");
		/*Ocultar elementos en el panel que no tengan que ver con creacion de usuario */
		SetActiveAllByTag(false, TAG_USER_MENU_MAIN);
		SetActiveAllByTag(true, TAG_CREATE_NEW_USER_FORM);
	}

	public void OnClickCreateUser(){
		Debug.Log ("OnClickCreateUser has triggered");

		if(menuObjects.ContainsKey(TAG_CREATE_NEW_USER_FORM) && menuObjects[TAG_CREATE_NEW_USER_FORM] != null){
			Debug.Log("Existen "+((GameObject[])menuObjects[TAG_CREATE_NEW_USER_FORM]).Length+" con el tag "+TAG_CREATE_NEW_USER_FORM);

			User newUser = new User();
			foreach(GameObject current in (GameObject[])menuObjects[TAG_CREATE_NEW_USER_FORM]){

				if(current.name.Equals("NameField")){
					newUser.Name = current.GetComponentInChildren<Text>().text;
				}
				if(current.name.Equals("LastNameField")){
					newUser.LastName = current.GetComponentInChildren<Text>().text;
				}
			}
		}else{
			Debug.Log("No existe un grupo de elementos con el tag ["+TAG_CREATE_NEW_USER_FORM+"]");
		}

	}

	private void SetActiveAllByTag(bool active, string TAG ){
		//Debug.Log(GameObject.FindGameObjectsWithTag(tag).Length);
		foreach( GameObject current in (GameObject[])menuObjects[TAG] ){
			current.gameObject.SetActive(active);
		}
	}

}
