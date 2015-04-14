using UnityEngine;
using System.Collections;

public class Huge_UI : MonoBehaviour {

	public GameObject[] UI;

	// Use this for initialization
	void Awake () {

		UI = GameObject.FindGameObjectsWithTag ("UI");
	
	}
	void OnMouseDown()
	{
		foreach (GameObject button in UI) {
			button.gameObject.GetComponent<Animator> ().SetTrigger ("out");
		}
	}

}
