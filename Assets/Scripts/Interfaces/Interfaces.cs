using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
    {
    void Killed();
    void Damaged(int damage);
    }

public interface IDetectable
{
    bool GetDetectable();
}