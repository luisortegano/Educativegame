using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class FindFigure : MonoBehaviour {
	
	float time = 1.0f;
	float timeLeft = 1.0f;
	
	public RawImage image;
	
    public void setFindFigure()
    {
        ImageCenter ic = GameObject.FindGameObjectWithTag("ImageCenter").GetComponent<ImageCenter>();
        string texturePath = ic.getRandomImage();

        if (File.Exists(texturePath))
        {
            var bytes = File.ReadAllBytes(texturePath);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);
            image.texture = tex;
        }
    }
    
    void Update (){
		if (0 < timeLeft){
			timeLeft -= Time.deltaTime;
		}else{
			setFindFigure();
			timeLeft = time;
		}
    }
}
