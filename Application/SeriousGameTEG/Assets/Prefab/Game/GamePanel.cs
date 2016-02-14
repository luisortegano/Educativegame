using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GamePanel : MonoBehaviour {
	public int GameId;
	public Text NameText;
	public Text CategoriaText;
	public Button SelectLevelButton;

	public void selectGameButtonAction (){
		Debug.Log ( "The game " + this.NameText.text + " with id: " + this.GameId + " was selected");
	}
}
