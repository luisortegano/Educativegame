using UnityEngine;
using UnityEngine.UI;
using ORM;
using System.Collections;

public class ChartPanel : MonoBehaviour {

	private UserInterfaceManager uim;
	public Button backButton;
	public int IdChartUser;

	public GameObject OptionPanelRoot;				//Panel to display widgets
	private GameObject UserOptionPanelObject;		//
	private GameObject ReportSelectionPanelObject;
	private GameObject ReportOptionsPanelObject;

	public GameObject ChartsOptionPanelObject;  /**DEPRECATED**/

	public string URL;
	WebViewObject webViewObject;
	public GameObject ViewChartPanelObject;

	public void CreateWebView (){
		Debug.Log("##### The method CreateWebView was reached");

		//Crear GO y ponerle el componente de WVO
		webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		webViewObject.Init(enableWKWebView:true); // Inicializar WVO sin script

		//Valores a Ojo % 
		webViewObject.SetMargins(Mathf.RoundToInt(Screen.width*0.39f), Mathf.RoundToInt(Screen.height*.12f), 10, 10);
		webViewObject.SetVisibility(true);

		StartCoroutine(loadURL());


//		if (URL.StartsWith("http")) {
//            webViewObject.LoadURL(URL.Replace(" ", "%20"));
//        } else {
//			var src = System.IO.Path.Combine(Application.streamingAssetsPath, URL);
//			var dst = System.IO.Path.Combine(Application.persistentDataPath, URL);
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

	}

	public void loadURL(string page){
		URL = page;
		StartCoroutine(loadURL());
	}

	private IEnumerator loadURL (){
		if (URL.StartsWith("http")) {
			webViewObject.LoadURL(URL.Replace(" ", "%20"));
        } else {
            var exts = new string[]{
                ".html"  // should be last
            };
            foreach (var ext in exts) {
				var url = URL.Replace(".html", ext);
                var src = System.IO.Path.Combine(Application.streamingAssetsPath, url);
                var dst = System.IO.Path.Combine(Application.persistentDataPath, url);
                byte[] result = null;
                if (src.Contains("://")) {
                    var www = new WWW(src);
                    yield return www;
                    result = www.bytes;
                } else {
                    result = System.IO.File.ReadAllBytes(src);
                }
                System.IO.File.WriteAllBytes(dst, result);
                if (ext == ".html") {
                    webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
                    break;
                }
            }
        }
        yield break;
	}

	public void DestroyWebView (){
		GameObject rootObject = webViewObject.gameObject;
		DestroyImmediate(webViewObject.GetComponent<WebViewObject>());
		DestroyImmediate(rootObject);
		webViewObject=null;
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
		CreateWebView();
	}

	void OnDisable (){
		DestroyWebView();
	}
	
	public void clickBackToHome(){
		destroyUserOption();
		destroyReportSelection();
		destroyReportOption();
		getUIM().MenuSetActive(Menu.ChartPanel,false);
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
		hideReportSelection();
		hideReportOption();

		//Find User values 
		UserSQLite userSQL = new UserSQLite ();
		User user = userSQL.getUserData(this.IdChartUser);

		//Create Panel If not exits
		if( UserOptionPanelObject == null ){
			UserOptionPanelObject = Instantiate(Resources.Load("UserOptionPanelPrefab", typeof (GameObject))) as GameObject;
			UserOptionPanelObject.transform.SetParent(OptionPanelRoot.gameObject.transform, false);
		}
		UserOptionPanelObject.SetActive(true);

		UserOptionalPanel scriptUserOptionPanel = UserOptionPanelObject.GetComponent<UserOptionalPanel>();
		scriptUserOptionPanel.setUserInfo(user);
	}

	public void destroyUserOption(){
		if( UserOptionPanelObject == null ) return;
		DestroyImmediate(UserOptionPanelObject);
	}

	public void hideUserOption(){
		if( UserOptionPanelObject == null ) return;
		UserOptionPanelObject.SetActive(false);
	}

	public void DisplayReportSelection (){
		//Hide other options sub-panels
		hideUserOption();
		hideReportOption();
		ChartsOptionPanelObject.SetActive(false);

		//Create Panel If not exits
		if( ReportSelectionPanelObject == null ){
			ReportSelectionPanelObject = Instantiate( Resources.Load("ReportPanel",typeof(GameObject))) as GameObject;
			ReportSelectionPanelObject.transform.SetParent(OptionPanelRoot.gameObject.transform, false);
		}
		ReportSelectionPanelObject.SetActive(true);
	}

	public void destroyReportSelection(){
		if( ReportSelectionPanelObject == null ) return;
		DestroyImmediate(ReportSelectionPanelObject);
	}

	public void hideReportSelection(){
		if( ReportSelectionPanelObject == null ) return;
		ReportSelectionPanelObject.SetActive(false);
	}

	public void DisplayReportOption (){
		//Hide other options sub-panel
		hideUserOption();
		hideReportSelection();

		//Display Option Charts
		// OLD ChartsOptionPanelObject.SetActive(true);

		if(ReportSelectionPanelObject==null)return;
		//Find name of Report prefab
		string prefab = ReportSelectionPanelObject.GetComponent<ReportPanel>().getNamePrefabOfSelectedReport();

		Debug.Log("##### Creating Report of prefab " + prefab);

		//Create Panel If not exits
		if( ReportOptionsPanelObject == null ){
			ReportOptionsPanelObject = Instantiate(Resources.Load(prefab, typeof (GameObject))) as GameObject;
			ReportOptionsPanelObject.transform.SetParent(OptionPanelRoot.gameObject.transform, false);
		}
		ReportOptionsPanelObject.GetComponent<Report>().setUserId(this.IdChartUser);
		ReportOptionsPanelObject.SetActive(true);
	}

	public void destroyReportOption(){
		if( ReportOptionsPanelObject == null ) return;
		DestroyImmediate(ReportOptionsPanelObject);
	}

	public void hideReportOption(){
		if( ReportOptionsPanelObject == null ) return;
		ReportOptionsPanelObject.SetActive(false);
	}
}
