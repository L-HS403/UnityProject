using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        StatusController player = other.GetComponent<StatusController>();
        if (other.CompareTag("Player"))
        {
            player.DecreaseHP(damage);
        }
    }
}
