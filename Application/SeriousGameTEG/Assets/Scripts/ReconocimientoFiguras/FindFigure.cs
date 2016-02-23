using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindFigure : MonoBehaviour {
	
	float timeChangeImage = 2.0f;
	float timeLeft = 2.0f;
	
	public RawImage image;
	
	void Update()
	{
		if (0 < timeLeft){
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0){
				Debug.Log("Change Image");
				ImageCenter ic = GameObject.FindGameObjectWithTag("ImageCenter").GetComponent<ImageCenter>();
				string texture = ic.getRandomImage();
				Debug.Log(texture);
				if (System.IO.File.Exists(texture))
				{
					Debug.Log("Tenemos La imagen");
					
					var bytes = System.IO.File.ReadAllBytes(texture);
					var tex = new Texture2D(1, 1);
					tex.LoadImage(bytes);
					image.texture = tex;
				}else{
					Debug.Log("No se encontro imagen");
				}
				
			}
		}else{
			timeLeft = timeChangeImage;
		}
	}
}
