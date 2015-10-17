using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

	public AudioClip level2Music;

	private AudioSource source;

	// Use this for initialization
	public void Awake () {
		int children = transform.childCount;
		for (int i = 0; i < children; ++i)
			print("For loop: " + transform.GetChild(i));
		source = GetComponent<AudioSource>();
	}

	void OnLevelWasLoaded(int level) {
		if (level == 2){
			source.clip = level2Music;
			source.Play();
		}
	}
}
