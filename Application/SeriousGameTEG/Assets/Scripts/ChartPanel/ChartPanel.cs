using UnityEngine;
using UnityEngine.UI;
using ORM;
using System.Collections;
using System;

public class ChartPanel : MonoBehaviour {

	public static string INDEX_PAGE="home.html";

	private UserInterfaceManager uim;
	public Button backButton;
	public int IdChartUser;

	public GameObject OptionPanelRoot;				//Panel to display widgets
	private GameObject UserOptionPanelObject;		//
	private GameObject ReportSelectionPanelObject;
	private GameObject ReportOptionsPanelObject;

	public string URL;
	WebViewObject webViewObject;

	void OnEnable (){
		backButton.onClick.RemoveAllListeners();
		backButton.onClick.AddListener(() => clickBackToHome());
		CreateWebView();
	}

	void OnDisable (){
		DestroyWebView();
	}

	public void CreateWebView (){
		URL = ChartPanel.INDEX_PAGE;

		webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		webViewObject.Init(enableWKWebView:true); // Inicializar WVO sin script

		//Valores a Ojo % 
		webViewObject.SetMargins(Mathf.RoundToInt(Screen.width*0.39f), Mathf.RoundToInt(Screen.height*.12f), 10, 10);
		webViewObject.SetVisibility(true);

		loadURL(url+"?"+ DateTime.Now.ToString("yyyyMMddHHmmssfff"));
	}

	UserInterfaceManager getUIM(){
		if(uim==null) uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		return uim;
	}

	private void loadHomePage (){
		loadURL(ChartPanel.INDEX_PAGE);
	}

	public void loadURL(string page){
		Debug.Log(string.Format("##### Loading URL WITH PARAMETER [{0}]",page));
		URL = page;
		StartCoroutine(loadURL());
	}

	private IEnumerator loadURL (){
		Debug.Log(string.Format("##### Loading URL [{0}]",URL));
		if (URL.StartsWith("http")) {
			webViewObject.LoadURL(URL.Replace(" ", "%20"));
        } else {
        	string destination = System.IO.Path.Combine(Application.persistentDataPath, URL);
			webViewObject.LoadURL("file://" + destination.Replace(" ", "%20"));
        }
        yield break;
	}

	public void DestroyWebView (){
		if( webViewObject != null ){
			GameObject rootObject = webViewObject.gameObject;
			DestroyImmediate(webViewObject.GetComponent<WebViewObject>());
			DestroyImmediate(rootObject);
			webViewObject=null;
		}
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
		if (this.IdChartUser == UserId) return;
		this.IdChartUser = UserId;
		if(this.isDisplayedUserProperties())
			DisplayUserProperties();
		if(this.isDisplayedReportOption())
			DisplayReportOption();
	}


	public void DisplayUserProperties(){	//Jugador
		//Hide other options sub-panels
		hideReportSelection();
		hideReportOption();
		loadHomePage();

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

	public bool isDisplayedUserProperties(){
		return UserOptionPanelObject != null && UserOptionPanelObject.activeSelf;
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
		loadHomePage();

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

	public void DisplayReportOption (){	//ChartsOption
		//Hide other options sub-panel
		hideUserOption();
		hideReportSelection();

		if(ReportSelectionPanelObject==null)return;
		//Find name of Report prefab
		string prefab = ReportSelectionPanelObject.GetComponent<ReportPanel>().getNamePrefabOfSelectedReport();
		if(prefab == null)return;
		Debug.Log("##### Report of prefab " + prefab);

		//Create Panel If not exits
		if( ReportOptionsPanelObject == null ){
			ReportOptionsPanelObject = Instantiate(Resources.Load(prefab, typeof (GameObject))) as GameObject;
			ReportOptionsPanelObject.name = prefab;
			ReportOptionsPanelObject.transform.SetParent(OptionPanelRoot.gameObject.transform, false);
		}
		ReportOptionsPanelObject.GetComponent<Report>().setUserId(this.IdChartUser);
		ReportOptionsPanelObject.SetActive(true);
	}

	public bool isDisplayedReportOption(){
		return ReportOptionsPanelObject != null && ReportOptionsPanelObject.activeSelf;
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
