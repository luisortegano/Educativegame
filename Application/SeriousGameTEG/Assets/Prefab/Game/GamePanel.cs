using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ORM;

public class GamePanel : MonoBehaviour {
	public int GameId;
	public Text NameText;
	public Text CategoriaText;
	public Button SelectLevelButton;

	public void selectGameButtonAction (){
		GameObject UIM = GameObject.FindGameObjectWithTag("UIManager");
		UserInterfaceManager uim = UIM.GetComponent<UserInterfaceManager>();
		uim.MenuSetActive(Menu.GameContentPanel,false);
		uim.MenuSetActive(Menu.GameLevelContentPanel,true);
		uim.getMenuGameobject(Menu.GameLevelContentPanel).SendMessage("setGameIdSelected", this.GameId);

		Debug.Log ( "The game " + this.NameText.text + " with id: " + this.GameId + " was selected");
	}
}
