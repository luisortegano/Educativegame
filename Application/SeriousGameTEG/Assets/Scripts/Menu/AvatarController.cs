using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour {
	public GameObject UIManager;


	public void ToggleUserSelectionPanel(){
		UserInterfaceManager UIM = UIManager.GetComponent<UserInterfaceManager>();

		if (UIM.MenuActiveSelf (Menu.HomePanel) || UIM.MenuActiveSelf (Menu.UserSelectionPanel)) {
			if( !UIM.MenuActiveSelf(Menu.NewUserFormPanel) ){
				UIM.MenuSetActive(Menu.UserSelectionPanel,!UIM.MenuActiveSelf(Menu.UserSelectionPanel));
				UIM.MenuSetActive(Menu.HomePanel,!UIM.MenuActiveSelf(Menu.HomePanel));
			}
		}
	}
}