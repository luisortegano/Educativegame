using ORM;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ReportPanel : MonoBehaviour {

	public Dropdown ReportDropdownName;
	public Text Description;
	private List<ReportDTO> listReports;


	void OnEnable (){
		ReportSQLite reportSQL = new ReportSQLite ();
		listReports = reportSQL.getAllReports();
		List<string> reportNames = new List<string>();
		foreach(ReportDTO current in listReports){
			reportNames.Add(current.Name);
		}
		ReportDropdownName.AddOptions(reportNames);
		if(0 < listReports.Count )
			Description.text = listReports[ReportDropdownName.value].Description;
	}

	void OnDisable (){
		ReportDropdownName.ClearOptions();
		listReports.Clear();
	}

	void OnReportChanged (){
		if(0 < listReports.Count )
			Description.text = listReports[ReportDropdownName.value].Description;
	}

	public string getNamePrefabOfSelectedReport (){
		Debug.Log("listreposrtsCount="+listReports.Count + "|ReportDropdownName.value"+ReportDropdownName.value);
		if(0 < listReports.Count )
			return listReports[ReportDropdownName.value].Prefab;
	}


}
