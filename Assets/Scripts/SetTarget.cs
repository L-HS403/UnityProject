using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget : MonoBehaviour
{
    public MonsterStatus currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        MonsterStatus target = other.GetComponent<MonsterStatus>();
        if (target != null)
        {
            currentTarget = target;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentTarget != null && other.GetComponent<MonsterStatus>() == currentTarget)
        {
            currentTarget = null;
        }
    }
}
