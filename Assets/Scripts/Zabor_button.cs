using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zabor_button : MonoBehaviour {

	public GameObject PlAi;
    public GameObject Home;
    public bool zdanie;
    public string Tegg;
    public GameObject strObj;
    public List<GameObject> arrtargets;
    public GameObject[] targets;
    public int prioritet;


    void Awake()
    {
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
        gameObject.transform.localScale = new Vector3(4.5f, 4.3f, 1); //4.7,4.5,1
		//PlAi.GetComponent<PLMain> ().gozabor ();

        Home.GetComponent<Zamok_CPU>().doZada4a(arrtargets, strObj, prioritet);
        
	}

    void OnMouseUp()
    {
        gameObject.transform.localScale = new Vector3(4.7f, 4.5f, 1); //4.7,4.5,1
    }
}
