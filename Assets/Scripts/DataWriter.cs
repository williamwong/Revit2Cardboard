using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataWriter : MonoBehaviour {

	List<GameObject> revitElements = new List<GameObject>();
	List<string> elementIds = new List<string>();

	// Use this for initialization
	void Start () {


		GameObject[] allElements = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		foreach (GameObject element in allElements) {
			string elementName = element.name;
			var elementId = elementName.Split(new char[] { '[', ']' });
			if (elementId.Length > 1)
			{
				revitElements.Add(element);
				elementIds.Add(elementId[1]);
				Debug.Log ("Element found: " + elementId[1]);
			}
		}

		for (int i = 0; i < revitElements.Count; i++) {
			var collider = revitElements[i].GetComponent<BoxCollider>();
			var data = revitElements[i].GetComponent<ElementData>();
			var cardboard = revitElements[i].GetComponent<ElementTracker>();
			if (collider == null) {
				collider = revitElements[i].AddComponent<BoxCollider>();
				// collider.size = new Vector3(1,1,1);
			}
			if (data == null) {
				data = revitElements[i].AddComponent<ElementData>();
			}
			if (cardboard == null) {
				cardboard = revitElements[i].AddComponent<ElementTracker>();
			}
			data.ElementId = elementIds[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}