using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GenericPerDayChart{
	public class DataGenericPerDay {
		
	}

	public class DayChart{
		public string chartId;
		public string date;
		public Value[] values;

		public DayChart(string chartId, string date){
			this.chartId=chartId;
			this.date=date;
		}

		public void setValues( Value[] values){
			this.values=values;
		}

//		public void addValue(Value val){
//			if(values==null) values = new List<Value>();
//			values.Add(val);
//		}
	}

	public class Value{
		public string label;
		public int amount;
		public string color;

		public Value (){
			this.label="label";
			this.amount=0;
			this.color="#000";
		}

		public Value ( string label, int amount, string color ){
			this.label=label;
			this.amount=amount;
			this.color=color;
		}
	}
}