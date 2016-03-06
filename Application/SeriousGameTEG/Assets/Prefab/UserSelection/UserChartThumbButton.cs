using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using ORM;

public class UserChartThumbButton : MonoBehaviour {
	public Button button;
	public int userId;

	public RawImage avatarUser;

	public void setAsActualUserChart (){
		GameObject.FindGameObjectWithTag("ChartPanelRoot").SendMessage("setUserId",this.userId);
	}
}
