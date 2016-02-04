using UnityEngine;
using System.Collections;

public class CategoryContentPanel : MonoBehaviour {

	public GameObject UIManger;
	public GameObject categoryPanelContainer;

	void OnBecameVisible (){
		GameObject[] ListUsers = GameObject.FindGameObjectsWithTag("CategoryPanel");
		this.cleanCategoryContentPanel(ListUsers);
		//this.populateUsers();
	}

	public void cleanCategoryContentPanel(GameObject[] ListCategory){
		if(ListCategory!=null && 0 < ListCategory.Length ){
			foreach(GameObject current in ListCategory){
				DestroyImmediate(current.gameObject);
			}
		}
	}

	public void populateCategory(){
		for ( int i = 0 ; i < 4 ; i++ ){
			this.instantiateCategory();
		}
	}

	public void instantiateCategory () {
		GameObject newCategoryPanel = (GameObject)Instantiate(categoryPanelContainer); 
		CategoryPanel panel = newCategoryPanel.GetComponent<CategoryPanel>();
		panel.testText.text = "prueba";
		newCategoryPanel.transform.SetParent(gameObject.transform);
		newCategoryPanel.transform.localScale = new Vector3(1,1,1);
	}

	public void closeCategoryPanel (){
		UserInterfaceManager uim = UIManger.GetComponent<UserInterfaceManager>();
		if (uim != null) {
			uim.MenuSetActive(Menu.CategorySelectionPanel,false);
			uim.MenuSetActive(Menu.HomePanel,true);
		}
	}
}
