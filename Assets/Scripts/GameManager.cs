using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool startGame;
    public static GameManager instance = null;
    public List<GameObject> levels = new List<GameObject>();
    public List<GameObject> tempList = new List<GameObject>();
    public List<GameObject> backGrounds = new List<GameObject>();
    private PDDLManager pddlManager;

    public float time = 1;
    private float z = 0;
    public int amountOfLevels;
    public Queue<GameObject> levelOrder = new Queue<GameObject>();
    private void Start()
    {
        pddlManager = GetComponent<PDDLManager>();
        amountOfLevels = 0;
        Object[] list = Resources.LoadAll("LevelPieces", typeof(GameObject));
        Object[] list2 = Resources.LoadAll("Backgrounds", typeof(GameObject));
        foreach(GameObject i in list)
        {
            levels.Add(i);
        }

        foreach (GameObject j in list2)
        {
            backGrounds.Add(j);
        }
        
        for (int i = 0; i < 10; i++)
        {
            getRandomLevel(0);
            amountOfLevels++;
        }
        z = 0;
        GenerateProblem();
    }

    public void getRandomLevel(int difficulty)
    {
       // GameObject temp  = Instantiate(levels[Random.Range(0, levels.Count)], new Vector3(0, 0, z), Quaternion.identity) as GameObject;
        float angle = 360f / (float)20;

        Quaternion rotation = Quaternion.AngleAxis(amountOfLevels * angle, Vector3.up);
        Vector3 direction = rotation * Vector3.forward;

        Vector3 position = new Vector3(0,0,-15) + (direction * 20);
        GameObject temp = Instantiate(levels[Random.Range(0, levels.Count)], position, rotation) as GameObject;

        
        temp.GetComponent<Level>().name = "gameobject" + amountOfLevels;
        tempList.Add(temp);
        Instantiate(backGrounds[0], new Vector3(-5, 0, z), Quaternion.identity);

        z += 20;
        amountOfLevels++;
    }

    public void StartAdd(string name)
    {
        StartCoroutine(add(name));
    }

    public IEnumerator add(string name)
    {
        foreach(GameObject l in tempList)
        {
            Debug.Log(name +" "+ l.GetComponent<Level>().name);
            if(l.GetComponent<Level>().name == name)
            {
                //                l.transform.position = new Vector3(0, 0, z);
                l.GetComponent<Level>().StartMovement(new Vector3(0, 0, z));
               // l.transform.rotation = Quaternion.identity;
                z += 20;
                Debug.Log("ADDED TO QUEUE");
                yield return null;
            }
        }
        yield return null;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }       
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GenerateProblem()
    {
        string enter = "\n";
        string file = "(define (problem problem0)" + enter;

        //Objects
        file += "(:objects" + enter + ";GameObjects" + enter;
        
        foreach(GameObject i in tempList)
        {
            file += i.GetComponent<Level>().name + " ";
        }

        file += "-GameObject" + enter;

        file += ";locations" + enter;

        int locationAmount = 0;

        file += "locationStart " + "locationEnd ";

        for(int j = 0; j < levels.Count; j++)
        {
            file += "location" + locationAmount + " ";
            locationAmount++;
        }

        file += "-location" + enter;

        file += enter + ")" + enter;



        //Init
        file += "(:init" + enter;
        string init = "";
        foreach (GameObject i in tempList)
        {
            file += "(unsorted " + i.GetComponent<Level>().name + ")" + enter;
            foreach (GameObject j in tempList)
            {
                if(j.GetComponent<Level>().difficulty > i.GetComponent<Level>().difficulty)
                {
                    init += "(above " + j.GetComponent<Level>().name + " " + i.GetComponent<Level>().name + ")" + enter;
                }
            }
        }

        file += init;

        file += ")" + enter;

        file += "(:goal" + enter + "(and" + enter;
        foreach (GameObject k in tempList)
        {
          file += "(not(unsorted " + k.GetComponent<Level>().name+ "))" + enter;       
        }

        file += ")" + enter + ")" + enter +")";

        using (StreamWriter sw = new StreamWriter("Assets/Resources/Files/Problem.txt"))
        {
            sw.Write(file);
        }

        StartCoroutine(pddlManager.Upload());
    }

    

}
