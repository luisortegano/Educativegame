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

	private AsyncOperation async;

	IEnumerator LoadLevelAditiveAs(int gi){
		async = SceneManager.LoadSceneAsync(gi,LoadSceneMode.Additive);
		while(!async.isDone){
			yield return null;
		}
		UserInterfaceManager uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterfaceManager>();
		Debug.Log(uim);

		if (GameObject.FindGameObjectWithTag("GameConfiguratorTag") != null ){
			GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setConfiguration",this.ConfigurationText.text);
			GameObject.FindGameObjectWithTag("GameConfiguratorTag").SendMessage("setGameLevelPanel",this);
			Debug.Log(string.Format("send to GameConfiguratorTag -> setConfiguration[{0}]",this.ConfigurationText.text));
		}else{
			Debug.Log("No hay configurator");
		}

		uim.MenuSetActive(Menu.Canvas,false);

	}

	public void clickPlay(){
		Debug.Log ("### Loading Game ("+GameId+")");
		StartCoroutine(LoadLevelAditiveAs(GameId));
	}
}
