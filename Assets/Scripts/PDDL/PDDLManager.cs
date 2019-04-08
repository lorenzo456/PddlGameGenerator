using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDDLManager : MonoBehaviour
{

    private string currentJsonFile = "";
    string url = "http://solver.planning.domains/solve";
     Queue<string> functions = new Queue<string>();
    GameManager gameManager;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    public IEnumerator Upload()
    {
        Debug.Log("UPLOAD");
        TextAsset domain = Resources.Load<TextAsset>("Files/Domain");
        TextAsset problem = (TextAsset)Resources.Load("Files/Problem", typeof(TextAsset));

        Debug.Log(domain.text);
        Debug.Log(problem.text);

        
        using (WWW www = new WWW(url))
        {
            yield return www;
            WWWForm form = new WWWForm();
            form.AddField("problem", problem.text);
            form.AddField("domain", domain.text);
            WWW w = new WWW(url, form);
            yield return new WaitForSeconds(3);
           // Debug.Log(w.text);
            currentJsonFile = w.text;
        }

        char[] delimiterChars = { '(', ':', ')', ' '};
        var node = JSON.Parse(currentJsonFile);
        for (int i = 0; i < 20; i++)
        {
            string n = node["result"][4][i]["action"];
            if(n == "null" || n == "Null" || n == null)
            {
                break;
            }

            string[] words = n.Split(delimiterChars);
            string functionName = "";
            bool start = false;
            foreach (var word in words)
            {
                if (word.Length > 1)
                {
                    if (word.Trim() == "precondition")
                        break;

                    if (word.Trim() == "action" || word.Trim() == "parameters")
                        continue;
                    functionName += word.Trim();
                    functionName += ",";
                   
                }
            }
            functionName = functionName.Remove(functionName.Length - 1);
            //functionName  += ")";
            functions.Enqueue(functionName);
        }

        StartCoroutine(CallFunctions());
        yield return null;
    }

    public IEnumerator CallFunctions()
    {
       // Debug.Log("START CALLING FUNCTIONS");
        foreach (string s in functions)
        {
            yield return new WaitForSeconds(.5f);
            string[] words = s.Split(',');
            if(words[0].Trim() == "add")
            {
               // Debug.Log("CALL ADDD");
                gameManager.StartAdd(words[1]);
            }
        }
        gameManager.startGame = true;
        yield return null;
    }

}
