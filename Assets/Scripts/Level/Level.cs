using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int difficulty = 0;
    public string name;
    public string type = "none";
    bool move = false;
    Vector3 target;
    public void StartMovement(Vector3 target)
    {
        move = true;
        this.target = target;
    }

    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
        }
    }
}
