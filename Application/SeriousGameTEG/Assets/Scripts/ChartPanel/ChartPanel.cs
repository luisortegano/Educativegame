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
	public GameObject ChartsOptionPanelObject;


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
		OptionPanelRoot.BroadcastMessage("CreateWebView");
	}
	
	public void clickBackToHome(){
		getUIM().MenuSetActive(Menu.ChartPanel,false);
		//getUIM().MenuSetActive(Menu.WebViewChart,false);
		GameObject wvc = GameObject.FindGameObjectWithTag("WebViewChart");
		if( wvc == null ){
			Debug.Log("############## NO se encontro objeto");
		}else{
			wvc.GetComponent<SampleWebView>().SetActiveWebView(false);
		}
		getUIM().MenuSetActive(Menu.HubPanel,true);
		getUIM().MenuSetActive(Menu.HomePanel,true);
	}

	public void setUserId (int UserId){
		Debug.Log("### The user was changed to: " + UserId );
		this.IdChartUser = UserId;
	}



	public void DisplayUserProperties(){
		Debug.Log("### UserId = "+ IdChartUser);

		//Hide other options sub-panels
		ChartsOptionPanelObject.SetActive(false);

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

		//Display Option Charts
		ChartsOptionPanelObject.SetActive(true);

	}
	
}
