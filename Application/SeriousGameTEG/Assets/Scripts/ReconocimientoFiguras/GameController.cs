using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject RF_ConfigurationGO;
	RF_Configuration rf_Config;
	
	RF_Configuration getRF_Config (){
		if(this.rf_Config==null){
			this.rf_Config = RF_ConfigurationGO.GetComponent<RF_Configuration>();
		}
		return this.rf_Config;
	}
	
	public GameObject imageCenter;
	ImageCenter ic;
	
	//Tablero
	GameObject gamePanel;
	//Velo
	public GameObject veloPanel;
	
	
	// For rotation purpouse
	float time = 5.0f;
	float timeLeft = 5.0f;
	
	// Cuenta regresiva
	float countDown = 100.0f;
	public Text countDownText;
	
	// Cuenta regresiva
	int points = 0;
	int fails = 0;
	public Text pointsText;
	
	//TiempoDeAlerta
	float alertTimeDefault = 0.5f;
	float alertTime = 0f;
	
	// Figura a encontrar
	public int toFindValue;
	public RawImage toFindImage;
	public int amountToFind;
	public int maxFails;
	

	public void initGameController (){
		ic = imageCenter.GetComponent<ImageCenter>();
	}
	
	public void setImagesTable (){
		System.Random r = new System.Random();
		int figureValue;
		//Imagen a encontrar
		this.toFindValue = r.Next(0,this.getRF_Config().AmountFigures);
		FigurePanel[] Figures = this.gameObject.GetComponentsInChildren<FigurePanel>();
		foreach( FigurePanel current in Figures ){
			figureValue = r.Next(0,this.getRF_Config().AmountFigures);
			while (figureValue == toFindValue){
				figureValue = r.Next(0,this.getRF_Config().AmountFigures);
			}
			current.setNewImage(ic.getBytesOfImageAtIndex(figureValue));
			current.imagePosition = figureValue;
		}
		int fp =  r.Next(0,Figures.Length);
		Figures[fp].setNewImage(ic.getBytesOfImageAtIndex(toFindValue));
		Figures[fp].imagePosition = toFindValue;
		setFindFigure();
	}
	
	void setFindFigure(){
		var tex = new Texture2D(1, 1);
		tex.LoadImage(ic.getBytesOfImageAtIndex(toFindValue));
		toFindImage.texture = tex;
	}
	
	void countDownUpdate(){
		countDown -= Time.deltaTime;
		countDownText.text = Mathf.Round(countDown).ToString();
	}
	
	void timeLeftUpdate(){
		timeLeft -= Time.deltaTime;
	}
	
	public void checkPairSelected (int selectedValue){
		if ( this.amountToFind == this.points || 0 < alertTime ) return;
		if (0 < countDown){
			if (selectedValue == toFindValue){
				this.points++;
				this.pointsText.text = points.ToString();
				addAlert(true);
			}else{
				this.fails++;
				addAlert(false);
			}
		}
	}
	
	void addAlert ( bool isGood ){
		Color colorAlert;
		if(isGood){
			colorAlert = new Color (0,255,0,.5f);
		}else{
			colorAlert = new Color (255,0,0,.5f);
		}
		this.gameObject.GetComponent<Image>().color = colorAlert;
		alertTime = alertTimeDefault;
	}
	
	void removeAlert (){
		alertTime -= Time.deltaTime;
		if( alertTime < 0 ){ 
			if(this.gameObject.GetComponent<Image>().color.g > 0){
				this.setImagesTable();
				timeLeft = time;
			}
			this.gameObject.GetComponent<Image>().color = new Color (0,0,0);
		}
	}
	
	void Update (){
		if( veloPanel.activeSelf ) return;
		Debug.Log("points="+this.points + " fails="+ this.fails + " maxFails="+ this.maxFails);
		if ( this.amountToFind == this.points || this.fails == this.maxFails )	{
			finishGame();
			return;
		}
	
		if( 0 < this.countDown  ) {
			removeAlert();
			countDownUpdate();
			timeLeftUpdate();
			if (timeLeft <= 0){
				setImagesTable();
				timeLeft = time;
			}
		}else if (!veloPanel.activeSelf){
			finishGame();
		}
	}

	public void finishGame(){
		getRF_Config().calculateExpendedTime((int)this.countDown);
		getRF_Config().setHits(this.points);
		Debug.Log("### SetFails = " + this.fails);
		getRF_Config().setFails(this.fails);
		getRF_Config().verifiedResult();
		getRF_Config().persistResults();

		/*Raise velo*/
		VeloPanel vp = veloPanel.GetComponent<VeloPanel>();
		vp.FinalText.text = getRF_Config().getResultMessage();

		/* Set Image of result */
		//this.gameObject.GetComponent<Image>().overrideSprite =  Resources.Load<Sprite>(string.Format("endGame/{0}",this.GameId));
		vp.endImage.overrideSprite = Resources.Load<Sprite>(string.Format("endGame/{0}",
			getRF_Config().isWin()? "ganaste":"perdiste"));
		veloPanel.SetActive(true);



		if(getRF_Config().isWin()){
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().playWin();
		}else{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().playLose();
		}

		return;
	}
	
	public void setCountDownTime (float time){
		this.countDown = time;
		this.countDownText.text = this.countDown.ToString();
	}
	
	public void setAmountToFind (int amount){
		this.amountToFind = amount;
	}

	public void setMaxFails (int maxFails){
		this.maxFails = maxFails;
	}
}