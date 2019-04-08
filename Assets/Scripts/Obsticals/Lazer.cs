using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        animator.SetFloat("Speed", GameManager.instance.time);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<IDetectable>().GetDetectable())
            {
                other.gameObject.GetComponent<IKillable>().Killed();
            }
        }
    }
}
