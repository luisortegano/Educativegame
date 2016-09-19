using System;
using UnityEngine;
using System.Collections.Generic;

namespace LastestResults
{
	[Serializable]
	public class DataLatestResults
	{
		public List<DayLastestResult> latestResults;

		public DataLatestResults (SortedDictionary<DateTime,List<LevelResultJson>> resultsPerDay){
			latestResults = new List<DayLastestResult> ();
			foreach(KeyValuePair<DateTime,List<LevelResultJson>> listResultsPerDay in resultsPerDay){
				latestResults.Add(new DayLastestResult (listResultsPerDay.Value,listResultsPerDay.Key));
			}
		}
	}

	[Serializable]
	public class DayLastestResult {
		public String date;
		public WinChart win;
		public LooseChart lose;

		public DayLastestResult (List<LevelResultJson> results, DateTime date){
			this.date = date.Date.ToString();
			this.win = new WinChart(results);
			this.lose = new LooseChart(results);
		}
	}

	[Serializable]
	public class WinChart {
		public float averageTime;
		public float averageFails;

		public WinChart ( List<LevelResultJson> results ){
			int wins = 0;
			float accumTimeAverage = 0;
			float accumFailsAverage = 0;
			foreach( LevelResultJson current in results ){
				if(current.winGame){
					wins++;
					accumTimeAverage += (float)current.expendedTime / (float)current.challengeTime;
					accumFailsAverage += (float)current.fails / (float)current.maxFails;
					Debug.Log(string.Format("##### expendedTime[{0}]/challengeTime[{1}]={2}", (float)current.expendedTime, (float)current.challengeTime,(float)current.expendedTime / (float)current.challengeTime));
					Debug.Log(string.Format("##### fails[{0}]/maxFails[{1}]={2}", (float)current.fails, (float)current.maxFails, (float)current.fails / (float)current.maxFails));
				}
			}
			if ( wins == 0){
				averageTime = averageFails = -1.0f;
			}else{
				averageTime = accumTimeAverage / (float)wins;
				averageFails = accumFailsAverage / (float)wins;
			}
		}
	}

	[Serializable]
	public class LooseChart {
		public float averageTime;
		public float averageHits;

		public LooseChart ( List<LevelResultJson> results ){
			int loses = 0;
			float accumTimeAverage = 0;
			float accumHitAverage = 0;
			foreach( LevelResultJson current in results ){
				if(!current.winGame){
					loses++;
					accumTimeAverage += (float)current.expendedTime / (float)current.challengeTime;
					accumHitAverage += (float)current.hits / (float)current.maxHits;
				}
			}
			if ( loses == 0){
				this.averageTime = this.averageHits = -1.0f;
			}else{
				this.averageTime = accumTimeAverage / (float)loses;
				this.averageHits = accumHitAverage / (float)loses;
			}
		}
	}

}

