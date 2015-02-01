// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ElementTracker : MonoBehaviour {
	private CardboardHead head;
	private Vector3 startingPosition;
	private ElementData data;
	private Color startingColor;
	private string displayText;
	
	void Start() {
		CardboardGUI.IsGUIVisible = true;
		CardboardGUI.onGUICallback += this.OnGUI;
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;


		startingColor = GetComponent<Renderer>().material.color;
	}
	
	void Update() {
		data = GetComponent<ElementData> ();

		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
		GetComponent<Renderer>().material.color = isLookedAt ? Color.green : startingColor;
		if (Cardboard.SDK.CardboardTriggered && isLookedAt) {
			selectItem(data.ElementId);
		}
	}

	void selectItem(string ElementId) {
		GetComponent<Renderer>().material.color = Color.blue;
		DataReader.getObjectInfo(ElementId);
		displayText = DataReader.dataToDisplay;
	}

	void OnGUI() {
		if (!CardboardGUI.OKToDraw(this)) {
			return;
		}
		GameObject.Find("InformationText").GetComponent<GUIText>().text = displayText;
	}
}
