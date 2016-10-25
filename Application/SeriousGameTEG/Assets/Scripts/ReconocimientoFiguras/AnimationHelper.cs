using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationHelper : MonoBehaviour {

	private Image helperImage;
	private int amountFig = 4;
	private int figure = 0;

	void Start () {
		this.helperImage = this.gameObject.GetComponent<Image>();
		StartCoroutine(changeImage());
	}

	public IEnumerator changeImage() {
		while(true){
			this.figure = (this.figure+1)%this.amountFig;
			this.helperImage.overrideSprite = Resources.Load<Sprite>(string.Format("reconocimientoFig/{0}",this.figure));
			yield return new WaitForSeconds(1.4f);
		}
	}
}
