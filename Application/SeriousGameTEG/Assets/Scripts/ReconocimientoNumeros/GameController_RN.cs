using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ORM;

public class GameController_RN : MonoBehaviour {

	public GameObject Configuration;

	public GameObject gameMainPanel;
	public GameObject gamePanel;
	public GameObject optionHolderPanel;
	public GameObject finishPanel;
	public GameObject finishImage;

	// GamePanel que mantiene los elementos de la lista deslizable 
	public GameObject cardsPanel;
	public GameObject cardPrefab;
	private Queue<GameObject> cardsQueue;


	// Timer Configuration
	public Text timerText;
	int timeGame;

	// Fail count
	int fails;

	// End Game flag
	bool endGameFlag = false;

	public void initGame(){
		this.fails = 0;
		this.timeGame = Configuration.GetComponent<Configuration_RN>().getChallengeTime();
		this.timerText.text = string.Format("{0}",this.timeGame);
		Debug.Log(string.Format("##### TimeGame was setted on: {0}", this.timeGame));
		this.gameMainPanel.SetActive(false);
		generateCards();
		setHubCard();
		this.gamePanel.SetActive(true);
		StartCoroutine(timerRoutine());
	}

	IEnumerator timerRoutine(){
		for(;;){
			if(timeGame <= 0 || this.endGameFlag){
				finishGame();
				break;
			}
			timeGame--;
			this.timerText.text = string.Format("{0}",this.timeGame);
			yield return new WaitForSeconds(1f);
		}
	}

	public void addFail(){
		this.fails++;
		if(fails == Configuration.GetComponent<Configuration_RN>().getMaxFails()){
			this.finishGame();
		}
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
		this.endGameFlag = true;


		this.finishPanel.SetActive(true);
		this.finishPanel.GetComponentInChildren<Text>().text = "Se acabo!";
		this.finishImage.GetComponent<Image>().overrideSprite =  Resources.Load<Sprite>(string.Format("endGame/{0}",
			this.cardsQueue.Count==0? "ganaste":"perdiste"));

		if(this.cardsQueue.Count==0){
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().playWin();
		}else{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().playLose();
		}

		UserManager um =  GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		Configuration_RN conf = Configuration.GetComponent<Configuration_RN>();

		LevelResultsSQLite levelResult = new LevelResultsSQLite();
		LevelResultRN levelResultRNJSON = new LevelResultRN (conf.challengeTime,conf.challengeTime-this.timeGame,conf.amountNumbers,
			conf.amountNumbers-this.cardsQueue.Count,conf.maxFails,this.fails, this.cardsQueue.Count==0  );

		levelResult.insertLevelResult(um.getUserSelected(), conf.gameLevelPanel.CodeLevel,this.cardsQueue.Count==0,JsonUtility.ToJson(levelResultRNJSON));
	}
}