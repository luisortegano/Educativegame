using UnityEngine;
using System;
using System.IO;
using System.Collections;
using ORM;

public class ThumbsPanel : MonoBehaviour {

	public static string USER_THUMB_RESOURCE_PATH = "UserChartThumbButton";
	private UserSQLite Usuarios;

	void OnEnable() {
		/*Clean and populate the Content Carousel*/
		GameObject[] ListUsers = GameObject.FindGameObjectsWithTag("UserThumbButton");
		this.cleanContentPanel(ListUsers);
		this.populateUsers();
		this.gameObject.GetComponentInChildren<UserChartThumbButton>().setAsActualUserChart();
	}

	public void cleanContentPanel(GameObject[] ListUsers){
		if(ListUsers!=null && 0 < ListUsers.Length ){
			foreach(GameObject current in ListUsers)
				DestroyImmediate(current.gameObject);
		}
	}

	public void populateUsers(){
		getUserSqlite().loadUsers();
		foreach( DataRow currentUser in getUserSqlite().Users.Rows )
			this.InstantiateUser(Convert.ToString(currentUser[UserSQLite.Name]),Convert.ToString(currentUser[UserSQLite.LastName]), Convert.ToInt32(currentUser[UserSQLite.Id]));
	}

	public void InstantiateUser (string name, string lastName, int UserId){
		GameObject newUserThumb = Instantiate(Resources.Load(ThumbsPanel.USER_THUMB_RESOURCE_PATH,typeof(GameObject))) as GameObject;

		UserChartThumbButton button = newUserThumb.GetComponent<UserChartThumbButton>();
		button.userId = UserId;
		byte[] textureBytes = File.ReadAllBytes(getUserSqlite().imagePathOfUser(UserId));
		var tex = new Texture2D(1, 1);
		tex.LoadImage(textureBytes);
		button.avatarUser.texture = tex;

		newUserThumb.transform.SetParent(gameObject.transform,false);
	}

	UserSQLite getUserSqlite () {
		if(this.Usuarios == null) this.Usuarios = new UserSQLite();
		return this.Usuarios;
	}
}
