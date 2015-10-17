using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ClickToLoadAsync : MonoBehaviour {

	public Slider LoadingBar;
	public GameObject Loadingimage;

	private AsyncOperation async;

	public void ClickAsync (int level) {
		Loadingimage.SetActive(true);
		StartCoroutine(LoadLevelWithBar(level));
	}

	IEnumerator LoadLevelWithBar (int level){
		async = Application.LoadLevelAsync(level);

		while(!async.isDone){
			LoadingBar.value = async.progress;
		}

		yield return null;
	}

}
