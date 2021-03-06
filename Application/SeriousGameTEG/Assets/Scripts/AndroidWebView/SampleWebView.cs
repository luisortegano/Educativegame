/*
 * Copyright (C) 2012 GREE, Inc.
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System.Collections;
using UnityEngine;

public class SampleWebView : MonoBehaviour
{
	public string Url;
//	public string SameDomainUrl;
//	public GUIText status;
	WebViewObject webViewObject;

	public IEnumerator CreateWebView (){
		Debug.Log("##### The method CreateWebView was reached");

		//Crear GO y ponerle el componente de WVO
		webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		webViewObject.Init(); // Inicializar WVO sin script

		webViewObject.SetMargins(Mathf.RoundToInt(Screen.width*0.4f), Mathf.RoundToInt(Screen.height*.25f), 5, 0 ); 
		webViewObject.SetVisibility(true);

		if (Url.StartsWith("http")) {
            webViewObject.LoadURL(Url.Replace(" ", "%20"));
        } else {
            var src = System.IO.Path.Combine(Application.streamingAssetsPath, Url);
            var dst = System.IO.Path.Combine(Application.persistentDataPath, Url);
            var result = "";
            if (src.Contains("://")) {
                var www = new WWW(src);
                yield return www;
                result = www.text;
            } else {
                result = System.IO.File.ReadAllText(src);
            }
            System.IO.File.WriteAllText(dst, result);
            webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
        }

	}

//	IEnumerator Start()
//	{
//		webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
//		webViewObject.Init((msg)=>{
//			Debug.Log(string.Format("CallFromJS[{0}]", msg));
////			status.text = msg;
////			status.GetComponent<Animation>().Play();
//		});
//		
//		webViewObject.SetMargins(Mathf.RoundToInt(Screen.width*0.4f), Mathf.RoundToInt(Screen.height*.25f), 5, 0 );
//		webViewObject.SetVisibility(true);
//
//        if (Url.StartsWith("http")) {
//            webViewObject.LoadURL(Url.Replace(" ", "%20"));
//        } else {
//            var src = System.IO.Path.Combine(Application.streamingAssetsPath, Url);
//            var dst = System.IO.Path.Combine(Application.persistentDataPath, Url);
//            var result = "";
//            if (src.Contains("://")) {
//                var www = new WWW(src);
//                yield return www;
//                result = www.text;
//            } else {
//                result = System.IO.File.ReadAllText(src);
//            }
//            System.IO.File.WriteAllText(dst, result);
//            webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
//        }
//	}

	public void SetActiveWebView(bool hideWebView){
		Debug.Log("###### visible?" + hideWebView.ToString());
		webViewObject.SetVisibility(hideWebView);
		Debug.Log("###### END visible?" + hideWebView.ToString());
	}


	bool change = false;
	public void changeSize () {
		if(change){
			webViewObject.SetMargins(5, 5, 5, Screen.height / 4);
		}else{
			webViewObject.SetMargins(5, 5, 5, 5);
		}
	}

	bool hide = true;
	public void hideWV () {
		webViewObject.SetVisibility(hide);
		hide = !hide;
	}


	bool destroy = true;
	public void fenixM () {
		if(destroy){
			webViewObject.SetVisibility(false);
			Destroy(webViewObject);
		}else{
//			Start();
		}
		destroy = !destroy;
	}


}
