using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class CameraController : MonoBehaviour {

	public Button CreateUserButton;
	public Button SaveButton;
	public RawImage Image;

	private WebCamDevice FrontCamera=default(WebCamDevice);
	private WebCamTexture wct;
	private bool cameraIsActive = false;

	// Use this for initialization
	void Start ()
	{
		if(0 < WebCamTexture.devices.Length){
			foreach(WebCamDevice wcd in WebCamTexture.devices ){
				if(wcd.isFrontFacing) this.FrontCamera = wcd;
			}
			
			if(FrontCamera.isFrontFacing){
				wct = new WebCamTexture (FrontCamera.name);
				Image.texture = wct;
				Image.material.mainTexture = wct;
				//wct.Play();
				this.cameraIsActive=true;
			}
		}
	}

	public void OnEnable (){
		//Activate camera
		if ( this.cameraIsActive )wct.Play();
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
				
				string relativeFilePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "capture.png";
				Debug.Log ( "###########SAVED ON: " + relativeFilePath );
				File.WriteAllBytes(relativeFilePath, snap.EncodeToPNG());
				CreateUserButton.gameObject.SetActive(true);
			}else{
				this.wct.Play();
				CreateUserButton.gameObject.SetActive(false);
			}
		}
	}

}
