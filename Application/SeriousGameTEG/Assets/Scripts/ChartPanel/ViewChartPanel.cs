using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewChartPanel : MonoBehaviour {

	public void UpdateChartPanel ( ){
		OptionsPanelManager OPM = GameObject.FindGameObjectWithTag("OptionChartManager").GetComponent<OptionsPanelManager>();
		InterfaceResultsManagerPrefabs IRM = GameObject.FindGameObjectWithTag("InterfaceResultsManagerPrefabs").GetComponent<InterfaceResultsManagerPrefabs>();
		GameObject ro = IRM.getResultsOf(OPM.getCurrentGame());
		if(ro == null){
			Debug.Log("###The game object was not create");
		}else{
			ro.transform.parent = this.gameObject.transform;
		}
	}

}
