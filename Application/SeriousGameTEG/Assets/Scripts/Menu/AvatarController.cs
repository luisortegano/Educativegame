using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour {

	public GameObject UserSelectionPanel;
	public GameObject NewUserFormPanel;

	public void ToggleUserSelectionPanel(){
		if( !this.NewUserFormPanel.activeSelf ){
			UserSelectionPanel.gameObject.SetActive(!UserSelectionPanel.gameObject.activeSelf);
		}
	}
}