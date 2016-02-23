using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;


public class ImageCenter : MonoBehaviour {
	
	string pathImage = "/Figuras";
	public List<string> ImagesURI;
	
	public string getRandomImage (){
		System.Random r = new System.Random();
		int i = r.Next(0,ImagesURI.Count);

        string imagePathSA = Application.streamingAssetsPath + pathImage + Path.AltDirectorySeparatorChar + ImagesURI[i];
        string imagePath = Application.persistentDataPath + pathImage + Path.AltDirectorySeparatorChar;

        /*Guardar imagen en carpeta con el numero de id del usuario */
        if (!Directory.Exists(imagePath)){
            Directory.CreateDirectory(imagePath);
        }

        imagePath = imagePath + ImagesURI[i];

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

        return imagePath;
	}
	
}
