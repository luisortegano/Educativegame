using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using ORM;

public class ContentPanelUserSelection : MonoBehaviour {

	/**/
	public GameObject UserSelectionPanel;

	/*New user Form*/
	public GameObject newUserFormPanel;

	public GameObject userThumbPrefab;

	UserSQLite Usuarios;

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
		
		getUserSqlite().loadUsers();
		//Debug.Log("Cantidad de usuarios" + getUserSqlite().Users.Rows.Count);
		foreach( DataRow currentUser in getUserSqlite().Users.Rows ){
			this.InstantiateUser(Convert.ToString(currentUser[UserSQLite.Name]),Convert.ToString(currentUser[UserSQLite.LastName]), Convert.ToInt32(currentUser[UserSQLite.Id]));
		}
	}

	public void InstantiateUser ( string name, string lastName, int UserId){
		Debug.Log("###STARTING CREATE USER "+ UserId +" ###");
		GameObject newUserThumb = (GameObject)Instantiate(userThumbPrefab); 
		UserThumbButton button = newUserThumb.GetComponent<UserThumbButton>();
		//button.nameLabel.text = name;
		//button.lastNameLabel.text = lastName;
		button.userId = UserId;

		byte[] textureBytes = File.ReadAllBytes(getUserSqlite().imagePathOfUser(UserId));
		var tex = new Texture2D(1, 1);
		tex.LoadImage(textureBytes);
		button.avatarUser.texture = tex;

		newUserThumb.transform.SetParent(gameObject.transform);
		newUserThumb.transform.localScale = new Vector3(1,1,1);
		Debug.Log("###FINISHING CREATE USER "+ UserId +" ###");
	}

	public void displayCreateUserForm (){
		this.UserSelectionPanel.SetActive(false);
		this.newUserFormPanel.SetActive(true);
	}

	UserSQLite getUserSqlite () {
		if(this.Usuarios == null)	this.Usuarios = new UserSQLite();
		return this.Usuarios;
	}

}
