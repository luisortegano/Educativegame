using System;
using UnityEngine;

[Serializable]
public class LevelResultJson{
	public int challengeTime;
	public int expendedTime ;
	public int hits;
	public int maxHits;
	public int fails;
	public int maxFails;
	public bool winGame;

	public LevelResultJson  (){}

	public LevelResultJson (int challengeTime, int expendedTime, int maxHits, int hits, int maxFails, int fails, bool winGame){
		this.challengeTime=challengeTime;
		this.expendedTime = expendedTime;
		this.maxHits = maxHits;
		this.hits=hits;
		this.maxFails=maxFails;
		this.winGame=winGame;
	}	
}