using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class CameraController : MonoBehaviour {

	public Button SaveButton;
	public RawImage Image;

	private WebCamDevice FrontCamera=default(WebCamDevice);
	private WebCamTexture wct;

	// Use this for initialization
	void Start ()
	{
		foreach(WebCamDevice wcd in WebCamTexture.devices ){
			if(wcd.isFrontFacing) this.FrontCamera = wcd;
		}

		if(FrontCamera.isFrontFacing){
			this.wct = new WebCamTexture (FrontCamera.name);
			Image.texture = wct;
			Image.material.mainTexture = wct;
			//this.wct.Play();
		}	
	}

	void Update (){

	}

	public void saveimage (){
		Texture2D snap = new Texture2D(wct.width, wct.height);
		snap.SetPixels(wct.GetPixels());
		snap.Apply();

		string relativeFilePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "capture.png";
		Debug.Log ( "###########SAVED ON: " + relativeFilePath );
		File.WriteAllBytes(relativeFilePath, snap.EncodeToPNG());
		//this.wct.Pause();
	}

}
