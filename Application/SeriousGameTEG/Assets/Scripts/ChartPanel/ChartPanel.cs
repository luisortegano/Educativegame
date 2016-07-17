using UnityEngine;
using UnityEngine.UI;
using ORM;
using System.Collections;

public class ChartPanel : MonoBehaviour {

	private UserInterfaceManager uim;
	public Button backButton;
	public int IdChartUser;

	public GameObject OptionPanelRoot;
	public GameObject UserOptionPanelObject;
	private GameObject ReportSelectionPanelObject;
	public GameObject ChartsOptionPanelObject;

	public string URL;
	GameObject supportWebViewObject;
	public GameObject ViewChartPanelObject;

	public IEnumerator CreateWebView (){
		Debug.Log("##### The method CreateWebView was reached");

		//Crear GO y ponerle el componente de WVO
		supportWebViewObject = new GameObject("WebViewObject");
		WebViewObject webViewObject = supportWebViewObject.AddComponent<WebViewObject>();
		webViewObject.Init(); // Inicializar WVO sin script

		//Valores a Ojo % 
		webViewObject.SetMargins(Mathf.RoundToInt(Screen.width*0.39f), Mathf.RoundToInt(Screen.height*.12f), 10, 10);
		webViewObject.SetVisibility(true);

		if (URL.StartsWith("http")) {
            webViewObject.LoadURL(URL.Replace(" ", "%20"));
        } else {
			var src = System.IO.Path.Combine(Application.streamingAssetsPath, URL);
			var dst = System.IO.Path.Combine(Application.persistentDataPath, URL);
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

	public void DestroyWebView (){
		DestroyImmediate(supportWebViewObject.GetComponent<WebViewObject>());
		DestroyImmediate(supportWebViewObject);
		supportWebViewObject=null;
	}

	UserInterfaceManager getUIM(){
		if(uim==null){
			uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		}
		return uim;
	}

	void OnEnable (){
		backButton.onClick.RemoveAllListeners();
		backButton.onClick.AddListener(() => clickBackToHome());

		// Crear WebView y activarlo
		StartCoroutine(CreateWebView());
	}

	void OnDisable (){
		DestroyWebView();
	}
	
	public void clickBackToHome(){
		getUIM().MenuSetActive(Menu.ChartPanel,false);
		//getUIM().MenuSetActive(Menu.WebViewChart,false);
//		GameObject wvc = GameObject.FindGameObjectWithTag("WebViewChart");
//		if( wvc == null ){
//			Debug.Log("############## NO se encontro objeto");
//		}else{
//			wvc.GetComponent<SampleWebView>().SetActiveWebView(false);
//		}
		getUIM().MenuSetActive(Menu.HubPanel,true);
		getUIM().MenuSetActive(Menu.HomePanel,true);
	}

	public void setUserId (int UserId){
		Debug.Log("### The user was changed to: " + UserId );
		this.IdChartUser = UserId;
	}

	public void DisplayUserProperties(){
		//Hide other options sub-panels
		ChartsOptionPanelObject.SetActive(false);
		destroyReportSelection();

		//Find User values 
		UserSQLite userSQL = new UserSQLite ();
		User user = userSQL.getUserData(this.IdChartUser);

		//Create Panel If not exits
		if( UserOptionPanelObject == null ){
			UserOptionPanelObject = Instantiate(Resources.Load("UserOptionPanelPrefab", typeof (GameObject))) as GameObject;
			UserOptionPanelObject.transform.SetParent(OptionPanelRoot.gameObject.transform, false);
		}

		UserOptionalPanel scriptUserOptionPanel = UserOptionPanelObject.GetComponent<UserOptionalPanel>();
		scriptUserOptionPanel.setUserInfo(user);
	}

	public void destroyUserOption(){
		if( UserOptionPanelObject == null ) return;
		DestroyImmediate(UserOptionPanelObject);
		UserOptionPanelObject = null;
	}

	public void DisplayChartsOption (){
		//Hide other options sub-panel
		destroyUserOption();
		destroyReportSelection();

		//Display Option Charts
		ChartsOptionPanelObject.SetActive(true);

	}

	public void DisplayReportSelection (){
		//Hide other options sub-panels
		destroyUserOption();
		ChartsOptionPanelObject.SetActive(false);

		//Create Panel If not exits
		if( ReportSelectionPanelObject == null ){
			ReportSelectionPanelObject = Instantiate( Resources.Load("ReportPanel",typeof(GameObject))) as GameObject;
			ReportSelectionPanelObject.transform.SetParent(OptionPanelRoot.gameObject.transform, false);
		}
	}

	public void destroyReportSelection(){
		if( ReportSelectionPanelObject == null ) return;
		DestroyImmediate(ReportSelectionPanelObject);
	}
	
}
