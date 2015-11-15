using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ContentPanelUserSelection : MonoBehaviour {

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
		user Usuarios = new user();
		Usuarios.loadUsers();
		foreach( DataRow currentUser in Usuarios.Users.Rows ){
			this.InstantiateUser(Convert.ToString(currentUser[user.Name]),Convert.ToString(currentUser[user.LastName]), Convert.ToInt32(currentUser[user.Id]));
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
}
