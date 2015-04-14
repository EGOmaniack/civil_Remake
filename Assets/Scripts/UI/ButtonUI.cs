using UnityEngine;
using System.Collections;

public class ButtonUI : MonoBehaviour {



	public GameObject[] buttons;
	private float x;
	private float y;
	
	
	void Awake()
	{
		x = gameObject.transform.localScale.x;
		y = gameObject.transform.localScale.y;
	
	}
	

	void OnMouseDown(){

		gameObject.transform.localScale = new Vector3(0.95f * x , 0.9f * y, 1);

		foreach (GameObject button in buttons) 
		{
			button.gameObject.GetComponent<Animator>().SetTrigger("intro");
		}
	}

	void OnMouseUp()
	{
		gameObject.transform.localScale = new Vector3(x, y, 1); //4.7,4.5,1
	}

}
