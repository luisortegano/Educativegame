using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class FigurePanel : MonoBehaviour {
	
	public RawImage figureImage;
	public int imagePosition;
	
	public void setNewImage(string pathImage){
		if (File.Exists(pathImage))
		{
			var bytes = File.ReadAllBytes(pathImage);
			var tex = new Texture2D(1, 1);
			tex.LoadImage(bytes);
			figureImage.texture = tex;
		}
	}
	
	public void setNewImage(byte[] imageBytes){
		var tex = new Texture2D(1, 1);
		tex.LoadImage(imageBytes);
		figureImage.texture = tex;
	}
	
	public void clickFigure(){
		GameObject.FindGameObjectWithTag("ImageCenter").SendMessage("checkPairSelected",imagePosition);
	}
	
}
