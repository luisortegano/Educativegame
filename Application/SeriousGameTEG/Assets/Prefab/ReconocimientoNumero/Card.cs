using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {

	public int numberCard;
	public Text numberCardText;
	public static Color basicColor = new Color (1f,1f,1f);

	IEnumerator fadeWarning (){
		yield return new WaitForSeconds(1f);
		this.gameObject.GetComponent<Image>().color = basicColor;
	}

	public void setWarning(Color warn){
		this.gameObject.GetComponent<Image>().color = warn;
		StartCoroutine("fadeWarning");
	}

	public void displayCardNumber (){
		this.numberCardText.text = this.numberCard.ToString();
	}

}
