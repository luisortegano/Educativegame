using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class FindFigure : MonoBehaviour {
	
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
}
