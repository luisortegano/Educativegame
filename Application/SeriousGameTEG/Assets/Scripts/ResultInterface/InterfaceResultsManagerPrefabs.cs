using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceResultsManagerPrefabs : MonoBehaviour {

	public List<GameObject> prefabsInterface;

	public GameObject getResultsOf (int IdUser, int IdGame, int Level){
		Debug.Log("### Requesting resultsOf " + IdGame);
		if( prefabsInterface.Count == 0 ||  prefabsInterface.Count <= IdGame ){
			return null;
		}
		GameObject prefab = Instantiate(prefabsInterface[IdGame]);
		GameObject res = prefab.GetComponent<IResult>().getResults(IdUser,IdGame+1,Level);
		Destroy(prefab);
		return res;
	}
	
}
