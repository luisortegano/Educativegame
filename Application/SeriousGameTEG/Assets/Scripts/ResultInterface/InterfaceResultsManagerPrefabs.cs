using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceResultsManagerPrefabs : MonoBehaviour {

	public List<GameObject> prefabsInterface;

	public GameObject getResultsOf (int indexInterface){
		Debug.Log("### Requesting resultsOf " + indexInterface);
		if( prefabsInterface.Count == 0 ||  prefabsInterface.Count <= indexInterface )	{
			return null;
		}

		GameObject prefab = Instantiate(prefabsInterface[indexInterface]);
		GameObject res = prefab.GetComponent<IResult>().getResults();
		Destroy(prefab);
		return res;
	}
	
}
