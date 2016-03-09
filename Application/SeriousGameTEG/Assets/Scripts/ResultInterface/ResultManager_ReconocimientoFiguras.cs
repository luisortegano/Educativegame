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
		DataTable dt = this.getResult().getLevelResults(IdUser, IdGame, Level);

		List<LevelResultJson> resultados = new List<LevelResultJson>();
		LevelResultJson lr;
		Debug.Log("### Retrive results count = " + dt.Rows.Count);
		foreach(DataRow current in dt.Rows){
			lr = new LevelResultJson ();
			JsonUtility.FromJsonOverwrite((string)current[LevelResultsSQLite.Result],lr);
			Debug.Log("The following result was loaded [" + JsonUtility.ToJson(lr) + "]");
		}

		GameObject go = (GameObject)Instantiate(Resources.Load("GeneralResultText"));
		go.GetComponent<Text>().text= "aqui escupo los promedios";
		return go;
	}
}

