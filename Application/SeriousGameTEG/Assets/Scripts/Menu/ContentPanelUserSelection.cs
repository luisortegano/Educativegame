using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using ORM;

public class ContentPanelUserSelection : MonoBehaviour {

	/**/
	public GameObject UserSelectionPanel;

	/*New user Form*/
	public GameObject newUserFormPanel;

	public GameObject userThumbPrefab;

	void OnEnable() {
		/*Clean and populate the Content Carousel*/
		GameObject[] ListUsers = GameObject.FindGameObjectsWithTag("UserThumbButton");
		this.cleanContentPanel(ListUsers);
		this.populateUsers();
	}

	public void cleanContentPanel(GameObject[] ListUsers){
		if(ListUsers!=null && 0 < ListUsers.Length ){
			foreach(GameObject current in ListUsers){
				DestroyImmediate(current.gameObject);
			}
		}
	}

	public void populateUsers(){
		UserSQLite Usuarios = new UserSQLite();
		Usuarios.loadUsers();
		//Debug.Log("Cantidad de usuarios" + Usuarios.Users.Rows.Count);
		foreach( DataRow currentUser in Usuarios.Users.Rows ){
			this.InstantiateUser(Convert.ToString(currentUser[UserSQLite.Name]),Convert.ToString(currentUser[UserSQLite.LastName]), Convert.ToInt32(currentUser[UserSQLite.Id]));
		}
	}

	public void InstantiateUser ( string name, string lastName, int UserId){
		GameObject newUserThumb = (GameObject)Instantiate(userThumbPrefab); 
		UserThumbButton button = newUserThumb.GetComponent<UserThumbButton>();
		button.nameLabel.text = name;
		button.lastNameLabel.text = lastName;
		button.userId = UserId;
		newUserThumb.transform.SetParent(gameObject.transform);
		newUserThumb.transform.localScale = new Vector3(1,1,1);
	}

	public void displayCreateUserForm (){
		this.UserSelectionPanel.SetActive(false);
		this.newUserFormPanel.SetActive(true);
	}
}
