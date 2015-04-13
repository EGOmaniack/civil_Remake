using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zabor_button : MonoBehaviour {

    public GameObject Home;
    public bool zdanie;
    public string Tegg;
    public GameObject strObj;
    public List<GameObject> arrtargets = new List<GameObject>();
    GameObject[] targets;
    public int prioritet;
	private float x;
	private float y;


    void Awake()
    {
		x = gameObject.transform.localScale.x;
		y = gameObject.transform.localScale.y;

        if (!zdanie)
        {
            targets = GameObject.FindGameObjectsWithTag(Tegg);
            foreach(GameObject tar in targets)
            {
                arrtargets.Add(tar);
            }
        }
    }

	void OnMouseDown()
	{
        gameObject.transform.localScale = new Vector3(0.95f * x , 0.9f * y, 1); //4.7,4.5,1

        Home.GetComponent<Zamok_CPU>().doZada4a(arrtargets, strObj, prioritet);
        
	}

    void OnMouseUp()
    {
        gameObject.transform.localScale = new Vector3(x, y, 1); //4.7,4.5,1
    }
}
