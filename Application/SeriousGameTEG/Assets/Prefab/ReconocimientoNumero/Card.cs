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

	public void click (){
		GameObject GameController = GameObject.FindGameObjectWithTag("GameControllerSG");
		GameController_RN gc = GameController.GetComponent<GameController_RN>();
		if(gc.checkCard(this.numberCard)){
			this.setWarning(new Color(0f,1f,0f));
			this.numberCardText.text = this.numberCard.ToString();

		}else{
			this.setWarning(new Color(1f,0f,0f));
			gc.addFail();
		}
	}

}
