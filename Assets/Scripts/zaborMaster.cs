using UnityEngine;
using System.Collections;

public class zaborMaster : MonoBehaviour {

	public GameObject masterCreep;


	public void buildingfinish()
	{
		masterCreep.GetComponent<Creep_AI> ().donestroit ();
	}

}
