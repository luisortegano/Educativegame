﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Persistence;
using System.IO;

public class UserManager : MonoBehaviour {

	private int actualUserId = -1;

	public void setUserWithUserId (int UserId){
		SqliteDatabase sql = new SqliteDatabase (System.IO.Path.Combine(Application.persistentDataPath,"TEG_SG.db"));
		DataTable result = sql.ExecuteQuery("SELECT * FROM user WHERE id = "+UserId.ToString()+";");
		Debug.Log ("the reult count was " + result.Rows.Count);
		if( 0 < result.Rows.Count ){
			this.actualUserId = UserId;
		}
		Debug.Log ("the userid "+this.actualUserId+" was selected");
	}

	public int getUserSelected (){
		return this.actualUserId;
	}

	public bool isUserSelected (){
		return 0<=this.actualUserId;
	}
}
