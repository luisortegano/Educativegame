using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;


public class ImageCenter : MonoBehaviour {
	
	string pathImage = "/Figuras";
	public List<string> ImagesURI;
	
	// For rotation purpouse
	float time = 5.0f;
	float timeLeft = 5.0f;
	
	// Figura a encontrar
	public int toFindValue;
	public RawImage toFindImage;
	
	//Puntaje
	public int Points = 0;
	public Text pointsText;
	
	void Awake (){
		string imagePathSA;
		string imagePath;
		imagePath = Application.persistentDataPath + pathImage + Path.AltDirectorySeparatorChar;
		/*Guardar imagen en carpeta con el numero de id del usuario */
		if (!Directory.Exists(imagePath)){
			Directory.CreateDirectory(imagePath);
		}
		for(int i=0; i < ImagesURI.Count; i++){
			imagePathSA = Application.streamingAssetsPath + pathImage + Path.AltDirectorySeparatorChar + ImagesURI[i];
			imagePath = Application.persistentDataPath + pathImage + Path.AltDirectorySeparatorChar+ ImagesURI[i];
						
			if (!System.IO.File.Exists(imagePath))
			{
				Debug.Log("The image was not persistent");
				// game database does not exists, copy default db as template
				if (Application.platform == RuntimePlatform.Android)
				{
					// Must use WWW for streaming asset
					WWW reader = new WWW(imagePathSA);
					while (!reader.isDone) { }
					Debug.Log("Copying image From Streaming Assets (Android)");
					System.IO.File.WriteAllBytes(imagePath, reader.bytes);
				}
				else {
					Debug.Log("Copying image From Streaming Assets (NO Android)");
					System.IO.File.Copy(imagePathSA, imagePath, true);
				}
			}
		}
	}
	
	public string imageUri(int position){
		return Application.persistentDataPath + pathImage + Path.AltDirectorySeparatorChar + ImagesURI[position];
	}
	
	public string getRandomImage (){
		System.Random r = new System.Random();
		return imageUri(r.Next(0,ImagesURI.Count));
	}
	
	public void setImagesToTable (){
		System.Random r = new System.Random();
		toFindValue = r.Next(0,ImagesURI.Count);
		int next=-1;
		FigurePanel fp;
		List<int> Table = new List<int>();
		GameObject[] figures = GameObject.FindGameObjectsWithTag("FigurePanel");
		int fl = figures.Length;
		for(int i = 0; i < fl; i++){
			next = r.Next(0,ImagesURI.Count);
			while (next == toFindValue){
				next = r.Next(0,ImagesURI.Count);
			}
			fp = figures[i].GetComponent<FigurePanel>();
			fp.setNewImage(imageUri(next));
			fp.imagePosition = next;
		}
		fp = figures[r.Next(0,fl)].GetComponent<FigurePanel>();
		fp.setNewImage(imageUri(toFindValue));
		fp.imagePosition = toFindValue;
		setFindFigure(imageUri(toFindValue));
	}
	
	public void setFindFigure(string texturePath)
	{
		if (File.Exists(texturePath))
		{
			var bytes = File.ReadAllBytes(texturePath);
			var tex = new Texture2D(1, 1);
			tex.LoadImage(bytes);
			toFindImage.texture = tex;
		}
	}
	
	public void checkPairSelected (int selectedValue){
		if (selectedValue == toFindValue){
			Points++;
			pointsText.text = Points.ToString();
		}
	}
	
	void Update (){
		if (0 < timeLeft){
			timeLeft -= Time.deltaTime;
		}else{
			setImagesToTable();
			timeLeft = time;
		}
	}
	
}
