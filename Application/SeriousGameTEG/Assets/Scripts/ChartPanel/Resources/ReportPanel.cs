using ORM;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ReportPanel : MonoBehaviour {

	public Dropdown ReportDropdownName;
	public Text Description;

	void OnEnable (){
		Debug.Log("#### U mai gaaaah!");
		ReportSQLite reportSQL = new ReportSQLite ();
		List<ReportDTO> listReports = reportSQL.getAllReports();
		List<string> reportNames = new List<string>();
		foreach(ReportDTO current in listReports){
			reportNames.Add(current.Name);
		}
		ReportDropdownName.AddOptions(reportNames);
//		ReportDropdownName.RefreshShownValue();

		Debug.Log("#### U mai gaaaah FINISH!");
	}
}
