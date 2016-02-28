using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using ORM;

public class GameLevelPanel : MonoBehaviour {
	public Text LevelText;
	public Text ConfigurationText;
	public Button PlayButton;
	
	public int GameId;
	public int CodeLevel;
	public string cnf;

	private AsyncOperation async;

	void OnLevelWasLoaded(int level) {
		Debug.Log("El puto nivel fue cargado");
		/*
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		lr.insertLevelResult(um.getUserSelected(),CodeLevel);
		Debug.Log("AQUIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
		GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setConfiguration",this.ConfigurationText);
		GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setGameLevelPanel",this);
		uim.MenuSetActive(Menu.Canvas,false);
		*/
	}

	IEnumerator LoadLevelAditiveAs(int gi){
		async = SceneManager.LoadSceneAsync(gi,LoadSceneMode.Additive);
		while(!async.isDone){
			yield return null;
		}
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		Debug.Log(uim);
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		lr.insertLevelResult(um.getUserSelected(),CodeLevel);
		Debug.Log("AQUIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
		Debug.Log(GameObject.FindGameObjectWithTag("GameConfiguratorTag"));
		GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setConfiguration",this.ConfigurationText.text);
		GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setGameLevelPanel",this);
		uim.MenuSetActive(Menu.Canvas,false);

	}

	public void clickPlay(){
		Debug.Log ("Loading Game ("+GameId+")");

		Debug.Log(cnf);
		pConf aux = JsonUtility.FromJson(cnf,pConf);

		//StartCoroutine(LoadLevelAditiveAs(GameId));

		//SceneManager.LoadScene(GameId,LoadSceneMode.Additive);

		/*
		Application.LoadLevelAdditive(GameId);

		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		lr.insertLevelResult(um.getUserSelected(),CodeLevel);
		Debug.Log("AQUIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
		while ( GameObject.FindGameObjectWithTag("GameConfiguratorTag") == null ){}
		GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setConfiguration",this.ConfigurationText);
		GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setGameLevelPanel",this);
		uim.MenuSetActive(Menu.Canvas,false);

		/*
		//Demostration of avance in levels
		LevelResultsSQLite lr = new LevelResultsSQLite();
		UserManager um = GameObject.FindGameObjectWithTag("ConfigurationObject").GetComponent<UserManager>();
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		lr.insertLevelResult(um.getUserSelected(),CodeLevel);
		uim.getMenuGameobject(Menu.GameLevelContentPanel).SendMessage("refreshView");
		*/
	}
}
