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

		/*FTP*/
		GameLevelSQLite gl = new GameLevelSQLite ();
		DataTable dt = gl.getLevelsOfGame (1);
		foreach (DataRow dr in dt.Rows) {
			Debug.Log( "Level: " + dr[GameLevelSQLite.Level] + ", id game" + dr[GameLevelSQLite.Id_Game] );
		 }
		/*FTP*/

		Debug.Log ( "The game " + this.NameText.text + " with id: " + this.GameId + " was selected");
	}
}
