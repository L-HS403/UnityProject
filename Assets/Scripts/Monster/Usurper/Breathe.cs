using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StatusController player = other.GetComponent<StatusController>();
        if (other.CompareTag("Player"))
        {
            player.DecreaseHP(5);
        }
    }
}
