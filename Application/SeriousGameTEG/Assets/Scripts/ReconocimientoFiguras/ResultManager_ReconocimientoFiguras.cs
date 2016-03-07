using UnityEngine;
using System.Collections;
using ORM;

public class ResultManager_ReconocimientoFiguras : MonoBehaviour
{
	private LevelResultsSQLite results;

	LevelResultsSQLite getResult(){
		if(this.results==null) this.results = new LevelResultsSQLite ();
		return this.results;
	}


	void loadResults (int UserId, int GameId, int Level){
		DataTable results = this.getResult().getLevelResults(UserId,GameId, Level);
		LevelResultJson lrj = new LevelResultJson ();
		foreach( DataRow current in results.Rows ){
			JsonUtility.FromJsonOverwrite((string)current[LevelResultsSQLite.Result],lrj);
			Debug.Log("###Values seted " + JsonUtility.ToJson(lrj));
		}
	}
}

