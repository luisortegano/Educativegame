using UnityEngine;
using UnityEngine.UI;
using ORM;
using System.Collections;

public class ChartPanel : MonoBehaviour {

	private UserInterfaceManager uim;
	public Button backButton;
	public int IdChartUser;

	public GameObject UserOptionPanel;
	public GameObject ChartsOptionPanel;


	UserInterfaceManager getUIM(){
		if(uim==null){
			uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		}
		return uim;
	}

	void OnEnable (){
		backButton.onClick.RemoveAllListeners();
		backButton.onClick.AddListener(() => clickBackToHome());
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
		ChartsOptionPanel.SetActive(false);

		//Find User values 
		UserSQLite userSQL = new UserSQLite ();
		User user = userSQL.getUserData(this.IdChartUser);

		// Remove all objects
		foreach( GameObject current in this.gameObject.GetComponentsInChildren<GameObject>()){
			DestroyImmediate(current);
		}

		// Insert Values of user

	}
	
}
