using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Persistence;

public class ContenPanelController : MonoBehaviour {

	public GameObject userThumbPrefab;

	void Awake () {

	}

	void Start (){
		GameObject go = GameObject.FindGameObjectWithTag("ConfigurationObject");
		UserManager umCO =  go.GetComponent<UserManager>();
		populateUsers(umCO.getUserList());
	}

	public void populateUsers(List<User> Users){
		int usersCount = GameObject.FindGameObjectsWithTag("UserThumbButton").Length;
		foreach (User current in Users){
			if(current.UserId < usersCount ) continue;
			this.InstantiateUser(current.Name,current.LastName, current.UserId);
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

	void OnEnable() {
		GameObject go = GameObject.FindGameObjectWithTag("ConfigurationObject");
		UserManager umCO =  go.GetComponent<UserManager>();
		populateUsers(umCO.getUserList());
	}
}
