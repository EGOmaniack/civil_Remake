﻿using UnityEngine;
using System.Collections;

public class Home_UI : MonoBehaviour {

	public GameObject cerkle;
	public GameObject[] buttons;
	public GameObject[] allbuttons;


	// Use this for initialization
	void Awake() 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		//gameObject.transform.localScale = new Vector3(0.95f * x , 0.9f * y, 1); //4.7,4.5,1
		cerkle.gameObject.GetComponent<Animator> ().SetTrigger ("GO");

		foreach (GameObject button in buttons) 
		{
			button.gameObject.GetComponent<Animator>().SetTrigger("intro");
		}
	}

}
