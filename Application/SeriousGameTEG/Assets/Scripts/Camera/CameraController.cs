using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;
using System.IO;


public class CameraController : MonoBehaviour {

	public static string temporalCapture;

	public Button CreateUserButton;
	public Button SaveButton;
	public RawImage Image;

	private WebCamDevice FrontCamera=default(WebCamDevice);
	private WebCamTexture wct;
	private bool cameraIsActive = false;

	// Use this for initialization
	void Start ()
	{
		temporalCapture = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "capture.png";
		if(0 < WebCamTexture.devices.Length){
			foreach(WebCamDevice wcd in WebCamTexture.devices ){
				if(wcd.isFrontFacing) this.FrontCamera = wcd;
			}
			
			if(FrontCamera.isFrontFacing){
				wct = new WebCamTexture (FrontCamera.name);
				Image.texture = wct;
				Image.material.mainTexture = wct;
				//Ajuste para camara frontal que se guarde y se vea de manera correcta
				Image.transform.localScale = new Vector3 (1,-1,1);
				this.cameraIsActive=true;
			}
		}
	}

	public void OnEnable (){
		//Activate camera
		if ( this.cameraIsActive ){
			wct.Play();
			this.CreateUserButton.gameObject.SetActive(false);
			/*
			Debug.Log ("########################################################");
			Debug.Log ("####################@OnEnable: the camera was activated");
			Debug.Log ("########################################################");
			*/
		}
	} 

	public void OnDisable (){
		//Deactivate camera
		if ( this.cameraIsActive )wct.Stop();
	}

	public void saveimage (){

		if (this.cameraIsActive){
			if( this.wct.isPlaying ){
				// take picture
				Texture2D snap = new Texture2D(wct.width, wct.height);
				this.wct.Pause();
				snap.SetPixels(wct.GetPixels());
				snap.Apply();

				Debug.Log("###########SAVED ON: " + CameraController.temporalCapture );
				File.WriteAllBytes(CameraController.temporalCapture, snap.EncodeToPNG());
				CreateUserButton.gameObject.SetActive(true);
			}else{
				this.wct.Play();
				CreateUserButton.gameObject.SetActive(false);
			}
		}
	}

}
