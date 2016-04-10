using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_RN : MonoBehaviour {

	public GameObject Configuration;

	public GameObject gameMainPanel;
	public GameObject gamePanel;
	public GameObject optionHolderPanel;

//	GamePanel que mantiene los elementos de la lista deslizable 
	public GameObject cardsPanel;
	public GameObject cardPrefab;
	private Queue<GameObject> cardsQueue;

	void mockConfiguration (){
		Configuration = new GameObject ();
		Configuration_RN config = Configuration.AddComponent<Configuration_RN>();
		config.amountNumbers=3;
	}

	public void initGame(){
		this.gameMainPanel.SetActive(false);
		mockConfiguration();
		generateCards();
		setFirstCard();
		this.gamePanel.SetActive(true);
	}

	public List<GameObject> Cards;
	public List<GameObject> getCards (){
		if(Cards == null)
			Cards = new List<GameObject>();
		return Cards;
	}

	void generateCards (){
		Configuration_RN config = this.Configuration.GetComponent<Configuration_RN>();

		List<GameObject> cards = getCards();
		cards.Clear();

		for(int i=0; i< config.amountNumbers; i++){
			cards.Add(instantiateGame(i));
		}

//		Desordenar lista
		for (int i = 0; i < cards.Count; i++) {
			GameObject temp = cards[i];
			int randomIndex = Random.Range(i, cards.Count);
			cards[i] = cards[randomIndex];
			cards[randomIndex] = temp;
		}

		this.cardsQueue = new Queue<GameObject> (cards);
	}

	void setFirstCard (){
		Debug.Log("### Set first Card");
		GameObject firstElement = cardsQueue.Peek();

		firstElement.transform.SetParent(this.optionHolderPanel.transform);

		firstElement.transform.localScale = new Vector3(1,1,1);

		firstElement.GetComponent<Card>().displayCardNumber();
		RectTransform rt = firstElement.GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0, 0);
		rt.anchorMax = new Vector2(1, 1);
		rt.pivot = new Vector2(0.5f, 0.5f);
		rt.localPosition = new Vector3(0f,0f,0f);
	}

	public GameObject instantiateGame (int NumberCard) {
		GameObject newCard = (GameObject)Instantiate(cardPrefab); 
		newCard.transform.SetParent(cardsPanel.gameObject.transform);
		newCard.transform.localScale = new Vector3(1,1,1);
		Card card = newCard.GetComponent<Card>();
		card.numberCard = NumberCard;
		return newCard;
	}

}
