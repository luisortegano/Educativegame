using System;
namespace ORM
{
	public class QueryUtils
	{
		public string allModel( string table ){
			return table+".*";
		}

		public string attributeFromTable (string table, string attribute ){
			return table + "." + attribute;
		}

		public string attributeFromTableTag (string table, string attribute ){
			return table + "_" + attribute;
		}

		public string innerJoin (string joinTable, string joinAttributte, string baseTable, string baseAttribute){
			return " inner join " + joinTable + " on " + attributeFromTable(baseTable,baseAttribute) +" = "+ attributeFromTable(joinTable,joinAttributte) +" ";
		}
		public string leftOuterJoin (string joinTable, string joinAttributte, string baseTable, string baseAttribute){
			return " left outer join " + joinTable + " on " + attributeFromTable(baseTable,baseAttribute) +" = "+ attributeFromTable(joinTable,joinAttributte) +" ";
		}

		public string equalsValue( string table, string attribute, int value ){
			return attributeFromTable (table, attribute) + " = " + value.ToString ();
		}
	}
}

