using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleJSON;

public class RobotScript : MonoBehaviour {
    /*
    bool atLocation;
    bool readyForNewAction;
    bool orderedToMove;

    Transform target;
    NavMeshAgent agent;
    public GameObject hand;
    public List<GameObject> targets = new List<GameObject>();
    int iterator = 0;
    public int maxIterations = 10;
    public GameObject desiredInteractable;

    JSONObject pddl = new JSONObject();
    public string functionName;
    public string parameter1;
    public string parameter2;
    public string parameter3;
    private string currentJsonFile ="";
    public bool moving;
    string url = "http://solver.planning.domains/solve";


    // Use this for initialization
    void Start () {
        StartCoroutine("Upload");
        agent = GetComponent<NavMeshAgent>();
        target = targets[0].GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {        
        if (checkIfAtLocation() == false && moving)
        {
            agent.destination = target.position;
        }
	}
    bool checkIfAtLocation()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.transform.position.x, 0, target.transform.position.z)) == 0)
        {
            atLocation = true;
            moving = false;
            if (orderedToMove)
            {
                Debug.Log("MoveENDED");
                StartCoroutine("nextAction");
                orderedToMove = false;
            }
        }
        else
        {
            atLocation = false;
            moving = true;
        }
        return atLocation;
    }

    public void moverobot()
    {
        foreach (GameObject t in targets)
        {
            if (t.name == parameter3)
            {
                target = t.GetComponent<Transform>();
                moving = true;
                orderedToMove = true;
            }
        }
    }

    public void pickupobject()
    {
        StartCoroutine("pickUp");
    }

    IEnumerator pickUp()
    {

        yield return new WaitForSeconds(3);

        Debug.Log("CALLED PICKUP");
        //desiredInteractable.GetComponent<Interactable>().isGrabbed = true;
        desiredInteractable.transform.position = hand.transform.position;
        desiredInteractable.transform.parent = hand.gameObject.transform;
        StartCoroutine("nextAction");
        
    }
    public void dropobject()
    {
        Debug.Log("CALLED dropobject");

        if (hand.gameObject.transform.GetChild(0).name == parameter2)
        {
            hand.gameObject.transform.GetChild(0).transform.position = target.transform.position;
           // hand.gameObject.transform.GetChild(0).GetComponentInChildren<Interactable>().isGrabbed = false;
            hand.gameObject.transform.GetChild(0).transform.parent = null;
        }

        StartCoroutine("nextAction");
    }
    IEnumerator nextAction()
    {
        Debug.Log("NEXT ACTION");
        yield return new WaitForSeconds(3);
        iterator++;
        readyForNewAction = true;
        StartCoroutine(iteratable(currentJsonFile));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if(other.name == parameter2)
            {
                desiredInteractable = other.gameObject;
            }
        }
    }




    public IEnumerator Upload()
    {
        TextAsset domain = Resources.Load<TextAsset>("UnityDomain3");
        TextAsset problem = Resources.Load<TextAsset>("UnityProblem3");

        using (WWW www = new WWW(url))
        {
            yield return www;
            WWWForm form = new WWWForm();
            form.AddField("problem", problem.text);
            form.AddField("domain", domain.text);
            WWW w = new WWW(url, form);
            yield return new WaitForSeconds(5);
            Debug.Log(w.text);
            currentJsonFile = w.text;
            StartCoroutine(iteratable(w.text));
        }

    }

    public IEnumerator iteratable(string jsonFile)
    {
        Debug.Log(iterator);
        emptyParameters();
        JSONNode node = JSON.Parse(jsonFile);
        JSONNode node2 = JSON.Parse(node[1].ToString());
        JSONNode node3 = JSON.Parse(node2[4].ToString());
        JSONNode node4 = JSON.Parse(node3[iterator].ToString());
        

        if (node4[1] == null || node4[1] == "null" || iterator == maxIterations)
        {
            Debug.Log("END");
            yield break;
        }
        Debug.Log(node4[1]);
        string temp = node4[1];

        functionName = temp.Split(new char[] { ' ' })[0];
        functionName = functionName.Split(new char[] { '(' })[1];
        parameter1 = temp.Split(new char[] { ' ' })[1];
        parameter2 = temp.Split(new char[] { ' ' })[2];
        parameter3 = temp.Split(new char[] { ' ' })[3];
        parameter3 = parameter3.Split(new char[] { ')' })[0];

        StartCoroutine(functionName);
    }

    void emptyParameters()
    {
        functionName = "";
        parameter1 = "";
        parameter2 = "";
        parameter3 = "";
    }
    */
}
