using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {

	public static Color basicColor = new Color (1f,1f,1f);

	IEnumerator fadeWarning (){
		yield return new WaitForSeconds(.5f);
		this.gameObject.GetComponent<Image>().color = basicColor;
	}


	public void setWarning(){
		this.gameObject.GetComponent<Image>().color = new Color (1f,0f,0f);
		StartCoroutine("fadeWarning");
	}
}
