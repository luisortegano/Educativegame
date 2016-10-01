using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip winSound;
	public AudioClip loseSound;
	private AudioSource source;
	private float volHighRange = 1.0f;

	void Start () {
		this.source = GetComponent<AudioSource>();
	}

	public void playWin (){
		this.source.PlayOneShot(winSound,1F);
	}

	public void playLose (){
		this.source.PlayOneShot(loseSound,1F);
	}
}
