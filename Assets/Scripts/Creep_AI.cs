using UnityEngine;
using System.Collections;

public class Creep_AI : MonoBehaviour {

	public bool svoboden = true;

    public float Idle_Time =10f;
    public float elapset_IdleT;
	public float speed = 10f;
	private NavMeshAgent nav;
	private Animator anim;
	public Transform somePoint;
	public bool idet;
	public bool hasbrevno;
	public Transform targetPoint;
	public bool gotoBrevno;
	GameObject brevnoHide;
	GameObject molotokHide;
	public string zada4a;
	public GameObject zadObj;
	bool gotocell;
	public GameObject[] brevnaviju;
    GameObject Home;
    public bool idu_domoy;
    Zamok_CPU cpu;
	public GameObject brevno;

	void Awake () 
	{
        Home = GameObject.Find("Home_01_01");
		brevnoHide = GameObject.Find ("/" + gameObject.name + "/Box008/Brevno");
		molotokHide = GameObject.Find ("/" + gameObject.name + "/CATRigHub001/CATRigSpine1/CATRigSpine2/CATRigSpine3/CATRigHub002/CATRigRArmCollarbone/CATRigRArm1/CATRigRArm2/CATRigRArmPalm/Molotok");
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
        cpu = GameObject.Find("Home_01_01").GetComponent<Zamok_CPU>();

	}

    void OnTriggerEnter(Collider other)                                 
    {
        if (other.name == "Home_01_01" && idu_domoy)                       // Залезаем в дом 
        {
            idu_domoy = false;
            Destroy(gameObject);
            cpu.creepInBase++;
            Creepishe ya = cpu.arrCreeps.Find(p => p.creep.name == gameObject.name);
            cpu.arrCreeps.Remove(ya);
        }
    }

	void Update () 
	{
		anim.SetFloat("speed", nav.velocity.magnitude);

		if(!idet)
		{
			idet =true;
			nav.SetDestination(somePoint.transform.position);

		}

		if (idet) 
		{

		}

		if(pathComplete() && gotoBrevno)
		{
			gotoBrevno = false;
			transform.LookAt(brevno.transform);								// Пришли к бревну
			brevnoHide.GetComponent<MeshRenderer>().enabled =true;
			Destroy(brevno);
			anim.SetBool("hasbrevno", true);
		}
		if(pathComplete() && gotocell)										//Пришли к цели
		{
			gotocell = false;
			transform.LookAt(zadObj.gameObject.transform);
			anim.SetTrigger("brevnodown");
		}
 

        if (elapset_IdleT < Idle_Time && svoboden == true)
        {
            elapset_IdleT += Time.deltaTime;
        }else if (elapset_IdleT>Idle_Time && svoboden == true && idu_domoy == false )
        
        {
            nav.SetDestination(Home.transform.position);
            idu_domoy = true;
        }


	}

	protected bool pathComplete()
	{
		if ( Vector3.Distance( nav.destination, nav.transform.position) <= nav.stoppingDistance)
		{
			if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
			{
				return true;
			}
		}
		
		return false;
	}
		
	
        public void sbros_idle()
        {
            elapset_IdleT = 0f;
            idu_domoy = false;
        }
		
		
	public void idemKceli()																	//Идем к цели. Запускаеся в конце анимации подъема бревна
	{
		targetPoint = zadObj.gameObject.GetComponentInChildren<TarPoint>().point;
		nav.SetDestination(targetPoint.transform.position);
		gotocell = true;
		anim.SetBool("hasbrevno", false);

	}

	public void build(GameObject zab, GameObject brevnishe) //получили задачу строить забор
	{
        sbros_idle();
		svoboden = false;
 		zada4a = "zaborbuild";
		zadObj = zab;

		if(!hasbrevno)
		{
			FindBrevno(brevnishe);
		}

	}

    public void FindBrevno(GameObject brevnoo)										//Поиск бревна
    {
        brevno = brevnoo;
        if (brevno != null)
        {										// Дебажный вариант
            gotoBrevno = true;
            idet = true;
            targetPoint = brevno.gameObject.GetComponentInChildren<TarPoint>().point;
            nav.SetDestination(targetPoint.transform.position);
            //brevno.GetComponent<svoboden_ili_kto>().svoboden = false;
        }
    }


	public void rabotaem ()										//Маслаем молотком
	{
		brevnoHide.GetComponent<MeshRenderer>().enabled = false;
		molotokHide.GetComponent<MeshRenderer> ().enabled = true;
	}

	public void zabanim()												// Включение анимации стоящегося объекта. Запускается анимацией крипа
	{
		zadObj.GetComponent<Animator> ().SetTrigger ("build");
		zadObj.GetComponent<zaborMaster> ().masterCreep = gameObject;
	}

	public void donestroit()                                            // Закончил
	{
		anim.SetTrigger ("done_stroit");
		molotokHide.GetComponent<MeshRenderer> ().enabled = false;
		svoboden = true;

       Creepishe miInlist = cpu.arrCreeps2.Find(p => p.creep.name == gameObject.name);
       miInlist.zada4a = "Stroim";
        miInlist.lastTask = "Stroim_zabor";
        //cpu.arrCreeps2.Remove(miInlist);
        //cpu.arrCreeps.Add(miInlist);

	}

}
