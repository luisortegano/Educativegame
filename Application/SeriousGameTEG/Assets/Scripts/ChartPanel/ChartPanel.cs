using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChartPanel : MonoBehaviour {

	private UserInterfaceManager uim;
	public Button backButton;
	public int IdChartUser;

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
		this.IdChartUser = UserId;
	}
	
}
