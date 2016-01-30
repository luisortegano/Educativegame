using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Persistence;
using System.IO;

public class UserManager : MonoBehaviour {

	private int actualUserId = -1;

	public void setUserWithUserId (int UserId){
		SqliteDatabase sql = new SqliteDatabase (System.IO.Path.Combine(Application.persistentDataPath,"TEG_SG.db"));
		DataTable result = sql.ExecuteQuery("SELECT * FROM user WHERE id = "+this.actualUserId.ToString()+";");
		if( 0 < result.Rows.Count ){
			this.actualUserId = UserId;
			DataRow current = result.Rows[0];
			Debug.Log (current[UserSQLite.Id]);
			Debug.Log (current[UserSQLite.Name]);
			Debug.Log (current[UserSQLite.LastName]);
		}
	}

	public bool isUserSelected (){
		Debug.Log("USERIDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD"+this.actualUserId);
		return 0<=this.actualUserId;
	}
}
