using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CameraController : MonoBehaviour {

	public Button SaveButton;
	public RawImage Image;

	private WebCamDevice FrontCamera=default(WebCamDevice);
	private WebCamTexture wct;

	// Use this for initialization
	void Start ()
	{
		/*
		WebCamTexture webcamTexture = new WebCamTexture();
		Image.texture = webcamTexture;
		Image.material.mainTexture = webcamTexture;
		webcamTexture.Play();
		*/

		foreach(WebCamDevice wcd in WebCamTexture.devices ){
			if(wcd.isFrontFacing) this.FrontCamera = wcd;
		}

		if(FrontCamera.isFrontFacing){
			wct = new WebCamTexture (FrontCamera.name);
			Image.texture = wct;
			Image.material.mainTexture = wct;
			wct.Play();
		}

		//Debug.Log ("Script has been started");
		//plane = GameObject.FindWithTag ("Player");
		//mCamera = new WebCamTexture ();
		//plane.GetComponent<Renderer>().material.mainTexture = mCamera;
			//.renderer.material.mainTexture = mCamera;
		//mCamera.Play ();	
	}
}
