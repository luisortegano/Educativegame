using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ORM;

public class ResultManager_ReconocimientoFiguras : MonoBehaviour, IResult
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


	public GameObject getResults (int IdUser, int IdGame, int Level){
		Debug.Log("### @getResults("+IdUser+","+IdGame+","+Level+")");
		DataTable dt = this.getResult().getLevelResults(IdUser, IdGame, Level);

		LevelResultJson lr;
		LevelResultJson avg = new LevelResultJson(0,0,0,0,0,0,false);
		Debug.Log("### Retrive results count = " + dt.Rows.Count);
		foreach(DataRow current in dt.Rows){
			lr = new LevelResultJson ();
			JsonUtility.FromJsonOverwrite((string)current[LevelResultsSQLite.Result],lr);
			Debug.Log("The following result was loaded [" + JsonUtility.ToJson(lr) + "]");
			avg = avg + lr;
		}
		float challengeTime = avg.challengeTime / dt.Rows.Count;
		float expendedTime = avg.expendedTime / dt.Rows.Count;
		float maxHits = avg.maxHits / dt.Rows.Count;
		float hits = avg.hits / dt.Rows.Count;
		float maxFails = avg.maxFails / dt.Rows.Count;
		float fails = avg.fails / dt.Rows.Count;


		GameObject go = (GameObject)Instantiate(Resources.Load("GeneralResultText"));
		go.GetComponent<Text>().text= "ChallengeTime = " + challengeTime +"\n"
			+ "expendedTime = " + expendedTime +"\n"
			+ "maxHits = " + maxHits +"\n"
			+ "hits = " + hits +"\n"
			+ "maxFails = " + maxFails +"\n"
			+ "fails = " + fails +"\n";
		return go;
	}
}

