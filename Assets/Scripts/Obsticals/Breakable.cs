using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IKillable
{
    public int Hp;

    public void Damaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Killed();
        }
    }

    public void Killed()
    {
        gameObject.SetActive(false);
    }
}
