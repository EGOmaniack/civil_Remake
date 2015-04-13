using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//я внутри матрицы
public class Creepishe
{
    public GameObject Zamok; //К какому замку принадлежит
    public GameObject creep; // рабочий
    public string zada4a;
    public string lastTask;
    public string id;


    public Creepishe(GameObject creepp, GameObject home) //Конструктор
    {
        this.creep = creepp;
        this.Zamok = home;
        this.zada4a = "Idle";
        this.lastTask = "Idle";
    }


}

public class Zada4isha
{
    public string name;  //название задачи
    public GameObject tar; // target объект стройки
    public List<Objectishe> arrtar; //перечень листовых объектов
    public List<Objectishe> arrtar2; //перечень разобранных крипами объектов
    public bool list;
    public int prioritet;
    static int zadNum = 0;

    public Creepishe creep1;
    public Creepishe creep2;
    public Creepishe creep3;

    public Zada4isha(int prior, bool lis)
    {
        this.prioritet = prior;
        this.list = lis;
        this.arrtar = new List<Objectishe>();
        this.arrtar2 = new List<Objectishe>();
        this.name = "Zada4a_" + zadNum;
        zadNum++;
    }

    public void Add_List(List<Objectishe> oni)
    {
		this.arrtar.Clear ();
        this.arrtar = oni;
    }

    public void Add_target(GameObject targ)
    {
        this.tar = targ;
    }


}

public class Objectishe
{
    public GameObject ObjectOBJ; //тут сообственно сам забор
    bool in_build_progress;  //в процессе строительства либо да либо нет
    GameObject worker;  // кто из рабочих мутит забор
    float persent_redy;  //процент готовности

    //внизу конструктор, он будет создавать этот забор с параметрами
    public Objectishe(GameObject obyect)
    {
        this.ObjectOBJ = obyect;
        this.in_build_progress = false;

    }
    //это будет добавлять к забору рабочего
    public void Add_worker(GameObject unit)
    {
        this.worker = unit;
        this.in_build_progress = true;
    }

}


public class Zamok_CPU : MonoBehaviour {

    public GameObject creepPref;
    public int creepInBase;
    public List<Creepishe> arrCreeps = new List<Creepishe>();
    public List<Creepishe> arrCreeps2 = new List<Creepishe>();
    public List<Objectishe> arrObjects = new List<Objectishe>();
    public List<Objectishe> arrObjects2 = new List<Objectishe>();
    public List<GameObject> arr_brevno = new List<GameObject>();
    public List<Zada4isha> arr_Zada4i = new List<Zada4isha>();
    GameObject[] objj;
    GameObject[] creeps;
    GameObject[] creepss;
    GameObject[] brevno;
    GameObject home;
    Animator anim;

    int numUnit = 55;
	// Use this for initialization
	void Awake () {
        home = gameObject;
  

        creepss = GameObject.FindGameObjectsWithTag("PlayerCreep");

        foreach (GameObject cr in creepss)
        {
            Creepishe cr_cr = new Creepishe(cr, home);
            arrCreeps.Add(cr_cr);
        }

        brevno = GameObject.FindGameObjectsWithTag("timber");

        foreach (GameObject brev in brevno)          //Заполняем лист бревнами
        {

            arr_brevno.Add(brev);


        }
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(arr_Zada4i.Count >0)
        {

            //Dotask(arr_Zada4i[0]);

            foreach (Zada4isha zad in arr_Zada4i)
            {

                if (zad.arrtar.Count > 0)
                {
                   Dotask(zad);
                   break;
                }
                
            }
        }

        if(Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log(arr_Zada4i.Count);
        }

	}


    public void creepNaZazada4u(GameObject creep, GameObject taskObj)
    {
        GameObject brevno = null;

        if (arr_brevno.Count > 0)
        {
            brevno = arr_brevno[0];
            arr_brevno.Remove(brevno);

        }
        if (brevno != null)
        {
            creep.gameObject.GetComponent<Creep_AI>().build(taskObj, brevno);
        }
        else
        {
            anim = creep.gameObject.GetComponent<Animator>();
           	anim.SetTrigger("hz");
        }
    }

    public Creepishe zapros_creepa(string Zadname)
    {
        foreach (Creepishe chk_creep in arrCreeps)
        {
            
                if (chk_creep.zada4a == "Idle")
                {
                    chk_creep.zada4a = "Stroim";
                    chk_creep.id = Zadname;
                    arrCreeps2.Add(chk_creep);
                    arrCreeps.Remove(chk_creep);
                    return( chk_creep);
                }
            
            

        }
        if (creepInBase > 0)
        {
            GameObject creep = Instantiate(creepPref, home.transform.position, home.transform.rotation) as GameObject;
            creep.name = "creep " + numUnit;
            Creepishe Ncreep = new Creepishe(creep, home);

            Ncreep.zada4a = "Stroim";
            Ncreep.id = Zadname;

            arrCreeps2.Add(Ncreep);
            creepInBase--;
            numUnit++;
            return (Ncreep);
        }
        return (null);
    }

    public void doZada4a(List<GameObject> arrObj, GameObject building, int  prioritet)
    {
        // Формируем задачу
       if(building == null)
       {
           Zada4isha zada4ka = new Zada4isha(prioritet, true);
           arrObjects.Clear();

           foreach(GameObject tar in arrObj)
           {
               Objectishe tar_tar = new Objectishe(tar);
               arrObjects.Add(tar_tar);
           }
           zada4ka.Add_List(arrObjects);

			zada4ka.creep1 = zapros_creepa(zada4ka.name);
			zada4ka.creep2 = zapros_creepa(zada4ka.name);
			zada4ka.creep3 = zapros_creepa(zada4ka.name);

			arr_Zada4i.Add(zada4ka);   // Добавляем задачу в список готовых к исполнению задач
       }
       else if (building != null)
       {
           Zada4isha zada4ka = new Zada4isha(prioritet, false);
           zada4ka.Add_target(building);

           zada4ka.creep1 = zapros_creepa(zada4ka.name);
           zada4ka.creep2 = zapros_creepa(zada4ka.name);
           zada4ka.creep3 = zapros_creepa(zada4ka.name);


           arr_Zada4i.Add(zada4ka);
       }

    } // Задача сформирована



    void Dotask(Zada4isha task)
    {

            if (task.arrtar.Count > 0 && (task.creep1 != null || task.creep2 != null || task.creep3 != null)) 
            {
                if (task.creep1.creep != null && task.arrtar.Count > 0 )
                {
                    if (task.creep1.zada4a == "Stroim")
                    {
                        foreach (Objectishe tar in task.arrtar)
                        {
                            creepNaZazada4u(task.creep1.creep, tar.ObjectOBJ);
                            task.creep1.zada4a = "zanyat";
                            task.arrtar2.Add(tar);
                            task.arrtar.Remove(tar);
                            break;
                        }
                    }

                }
                else { task.creep1 = zapros_creepa(task.name); }
                if (task.creep2.creep != null && task.arrtar.Count > 0)
                {
                    if (task.creep2.zada4a == "Stroim")
                    {
                        foreach (Objectishe tar in task.arrtar)
                        {
                            creepNaZazada4u(task.creep2.creep, tar.ObjectOBJ);
                            task.creep2.zada4a = "zanyat";
                            task.arrtar2.Add(tar);
                            task.arrtar.Remove(tar);
                            break;
                        }
                    }
                }
                else { task.creep2 = zapros_creepa(task.name); }
                if (task.creep3.creep != null && task.arrtar.Count > 0)
                {
                    if (task.creep3.zada4a == "Stroim")
                    {
                        foreach (Objectishe tar in task.arrtar)
                        {
                            creepNaZazada4u(task.creep3.creep, tar.ObjectOBJ);
                            task.creep3.zada4a = "zanyat";
                            task.arrtar2.Add(tar);
                            task.arrtar.Remove(tar);
                            break;
                        }
                    }
                }
                else { task.creep3 = zapros_creepa(task.name); }

            }
            else if (task.arrtar.Count == 0)
            {

                Zada4isha miInList = arr_Zada4i.Find(p => p.name == task.name);
                arr_Zada4i.Remove(miInList);
                
            }
        

    }





}
