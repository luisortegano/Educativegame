using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Persistence;

public class UserManager : MonoBehaviour {

	private int actualUserId;

	public void createUser(){
		/*
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
		*/
	}

	public void setUserWithUserId (int UserId){
		this.actualUserId = UserId;

		SqliteDatabase sql = new SqliteDatabase (System.IO.Path.Combine(Application.persistentDataPath,"TEG_SG.db"));
		DataTable result = sql.ExecuteQuery("SELECT * FROM user WHERE id = "+this.actualUserId.ToString()+";");
		if( 0 < result.Rows.Count ){
			DataRow current = result.Rows[0];
			Debug.Log (current[UserSQLite.Id]);
			Debug.Log (current[UserSQLite.Name]);
			Debug.Log (current[UserSQLite.LastName]);
		}

	}
}
