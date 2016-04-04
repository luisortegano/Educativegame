using UnityEngine;
using System.Collections;

public class GameController_RN : MonoBehaviour {

	public GameObject Configuration;

	public GameObject gameMainPanel;
	public GameObject gamePanel;

//	GamePanel que mantiene los elementos de la lista deslizable 
	public GameObject cardsPanel;
	public GameObject cardPrefab;

	void mockConfiguration (){
		Configuration = new GameObject ();
		Configuration_RN config = Configuration.AddComponent<Configuration_RN>();
		config.amountNumbers=3;
	}

	public void initGame(){
		this.gameMainPanel.SetActive(false);
		mockConfiguration();
		generateCards();
		this.gamePanel.SetActive(true);
	}

	void generateCards (){
		Configuration_RN config = this.Configuration.GetComponent<Configuration_RN>();
		for(int i=0; i< config.amountNumbers; i++){
			instantiateGame();
		}
	}

	public void instantiateGame () {
		GameObject newCard = (GameObject)Instantiate(cardPrefab); 
		newCard.transform.SetParent(cardsPanel.gameObject.transform);
		newCard.transform.localScale = new Vector3(1,1,1);
	}

}
