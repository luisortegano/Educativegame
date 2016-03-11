using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewChartPanel : MonoBehaviour {

	public void UpdateChartPanel ( ){
		this.cleanViewChart();

		OptionsPanelManager OPM = GameObject.FindGameObjectWithTag("OptionChartManager").GetComponent<OptionsPanelManager>();
		InterfaceResultsManagerPrefabs IRM = GameObject.FindGameObjectWithTag("InterfaceResultsManagerPrefabs").GetComponent<InterfaceResultsManagerPrefabs>();
		GameObject ro = IRM.getResultsOf(OPM.getCurrentUser(),OPM.getCurrentGame(),OPM.getCurrentLevel());

		if(ro == null){
			Debug.Log("###The game object was not create");
		}else{
			ro.transform.SetParent(this.gameObject.transform,false);
			Debug.Log("###The game object was create");
		}
	}

	public void cleanViewChart (){
		foreach (Transform child in transform) {
		     GameObject.Destroy(child.gameObject);
		}
	}

}
