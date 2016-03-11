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
		this.fails=fails;
		this.winGame=winGame;
	}

	public static LevelResultJson operator +(LevelResultJson c1, LevelResultJson c2) 
   {
		return new LevelResultJson(c1.challengeTime + c2.challengeTime, c1.expendedTime + c2.expendedTime, c1.maxHits + c2.maxHits,
			c1.hits + c2.hits, c1.maxFails + c2.maxFails, c1.fails + c2.fails, false);
   }
				
}