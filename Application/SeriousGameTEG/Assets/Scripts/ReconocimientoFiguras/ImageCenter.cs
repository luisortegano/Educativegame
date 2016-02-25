using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;


public class ImageCenter : MonoBehaviour {
	
	public GameObject RF_ConfigurationGO;
	RF_Configuration rf_Config;
	
	RF_Configuration getRF_Config (){
		if(this.rf_Config==null){
			this.rf_Config = RF_ConfigurationGO.GetComponent<RF_Configuration>();
		}
		return this.rf_Config;
	}
	
	//Carga de imagenes a memoria
	string pathImage = "/Figuras";
	public List<string> ImagesURI;
	List<byte[]> imageBytes = new List<byte[]>();
	
	public void loadImages (){
		string imagePathSA;
		Debug.Log("Cantidad de Imagenes =" + this.getRF_Config().AmountFigures);
		for(int i=0; i < this.getRF_Config().AmountFigures; i++){
			Debug.Log ("Cargando ["+ImagesURI[i]+"]");
			imagePathSA = Application.streamingAssetsPath + pathImage + Path.AltDirectorySeparatorChar + ImagesURI[i];
			WWW reader = new WWW(imagePathSA);
			while (!reader.isDone) { }
			Debug.Log("Copying image From Streaming Assets (Android)");
			imageBytes.Add(reader.bytes);
		}
	}
	
	public byte[] getBytesOfImageAtIndex (int index){
		if ( imageBytes.Count < index ) return null;
		return imageBytes[index];
	}
	
	
	
	
	
	
	
	
	
	
	//Puntaje
	public int Points = 0;
	public Text pointsText;
	
	public GameObject ContainerPanel;
	public GameObject StartButton;
	
	public void setFindFigure(string texturePath)
	{
		/*
		if (File.Exists(texturePath))
		{
			var bytes = File.ReadAllBytes(texturePath);
			var tex = new Texture2D(1, 1);
			tex.LoadImage(bytes);
			toFindImage.texture = tex;
		}
		*/
	}
	
	public void checkPairSelected (int selectedValue){
		/*
		if (selectedValue == toFindValue && 0 < countDown ){
			Points++;
			pointsText.text = Points.ToString();
		}
		*/
	}
	
	void Update (){
		/*
		if( 0 < countDown ) {
			countDown -= Time.deltaTime;
			countDownText.text = Mathf.Round(countDown).ToString();
			if (0 < timeLeft){
				timeLeft -= Time.deltaTime;
			}else{
				setImagesToTable();
				timeLeft = time;
			}
		}
		*/
	}
}
