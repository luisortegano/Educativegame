using UnityEngine;
using System.Collections.Generic;

public class RemoveLevel : MonoBehaviour {

	public List<GameObject> RemoveList;
	
	public void RemoveGame (){
		foreach(GameObject go in RemoveList){
			Destroy(go) ;
		}

		GameObject uim = GameObject.FindGameObjectWithTag("UIManager");
		if( uim != null ) uim.GetComponent<UserInterfaceManager>().MenuSetActive(Menu.Canvas,true);
		Destroy(this.gameObject);
	}
	
}
