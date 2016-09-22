using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController_RN : MonoBehaviour {

	public GameObject Configuration;

	public GameObject gameMainPanel;
	public GameObject gamePanel;
	public GameObject optionHolderPanel;
	public GameObject finishPanel;

	// GamePanel que mantiene los elementos de la lista deslizable 
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
		//mockConfiguration();
		generateCards();
		setHubCard();
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

		// Desordenar lista
		for (int i = 0; i < cards.Count; i++) {
			GameObject temp = cards[i];
			int randomIndex = Random.Range(i, cards.Count);
			cards[i] = cards[randomIndex];
			cards[randomIndex] = temp;
		}

		this.cardsQueue = new Queue<GameObject> (cards);
	}

	void changeCard (){
		cardsQueue.Enqueue(cardsQueue.Dequeue());
	}

	public void setHubCard (){
		this.changeCard();
		GameObject firstElement = cardsQueue.Peek();
		this.optionHolderPanel.GetComponentInChildren<Text>().text = firstElement.GetComponent<Card>().numberCard.ToString();
	}

	public bool checkCard( int checkNumber ){
		if (0 == cardsQueue.Count){
			return false;
		}

		bool check = cardsQueue.Peek().GetComponent<Card>().numberCard == checkNumber;
		if ( check ){
			cardsQueue.Dequeue();
			if (0<cardsQueue.Count ){
				GameObject firstElement = cardsQueue.Peek();
				this.optionHolderPanel.GetComponentInChildren<Text>().text = firstElement.GetComponent<Card>().numberCard.ToString();
			}else{
				this.optionHolderPanel.GetComponentInChildren<Text>().text = "-";
				this.finishGame();
			}
		}
		return check;
	}

	public GameObject instantiateGame (int NumberCard) {
		GameObject newCard = (GameObject)Instantiate(cardPrefab); 
		newCard.transform.SetParent(cardsPanel.gameObject.transform);
		newCard.transform.localScale = new Vector3(1,1,1);
		Card card = newCard.GetComponent<Card>();
		card.numberCard = NumberCard;
		return newCard;
	}

	void finishGame(){
		this.finishPanel.SetActive(true);
		this.finishPanel.GetComponentInChildren<Text>().text = "Se acabo!";
	}
}