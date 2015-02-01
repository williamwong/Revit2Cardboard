using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class DataReader {

	private static Dictionary<string, Dictionary<string,string>> mDict = new Dictionary<string, Dictionary<string,string>> ();
	public static string dataToDisplay = string.Empty;

	// Use this for initialization
	public static void Start () {

		mDict.Clear ();

		string[] lines = System.IO.File.ReadAllLines(@"RevitData.txt");


		foreach (var line in lines) {
			Dictionary<string,string> geoDict= new Dictionary<string, string>();
			string[] objectData = line.Split(',');  
			string elemId=string.Empty;
			foreach (var od in objectData) {

				string[] subdata= od.Split(':');
				if(subdata[0]=="ElementId"){
					elemId=subdata[1];
					continue;
				}
				geoDict.Add(subdata[0],subdata[1]);
			}
			mDict.Add (elemId,geoDict);
		}
		Debug.Log(mDict);
	
	}
	
	// Update is called once per frame
 void Update () {
	
	}

	public static void getObjectInfo(string ElementId){

		//extend this tomorrow with filters

		dataToDisplay = string.Empty;

		Dictionary<string, string> objectData = mDict[ElementId];
		foreach (string key in objectData.Keys) {

			dataToDisplay+= objectData[key] + "\n";
		}

	}
}
