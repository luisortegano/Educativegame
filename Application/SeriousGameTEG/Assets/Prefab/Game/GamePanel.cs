using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ORM;

public class GamePanel : MonoBehaviour {
	public int GameId;
	public Text NameText;
	public Text CategoriaText;
	public Button SelectLevelButton;

	public void setImage(){
		Debug.Log(string.Format("##### Loading {0}",string.Format("game/{0}",this.GameId)));
		if(Resources.Load<Sprite>(string.Format("game/{0}",this.GameId)) != null)
			Debug.Log(string.Format("##### {0} can be loaded",string.Format("game/{0}",this.GameId)));
		this.gameObject.GetComponent<Image>().overrideSprite =  Resources.Load<Sprite>(string.Format("game/{0}",this.GameId));
	}

	public void selectGameButtonAction (){
		GameObject UIM = GameObject.FindGameObjectWithTag("UIManager");
		UserInterfaceManager uim = UIM.GetComponent<UserInterfaceManager>();
		uim.MenuSetActive(Menu.GameContentPanel,false);
		uim.MenuSetActive(Menu.GameLevelContentPanel,true);
		uim.getMenuGameobject(Menu.GameLevelContentPanel).SendMessage("setGameIdSelected", this.GameId);
		Debug.Log ( string.Format("#####The game {0} with id: {1} was selected"), this.NameText.text, this.GameId);
	}
}
