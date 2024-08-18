using System.Collections;
using UnityEngine;

public class TerrorBringerBreath : MonoBehaviour
{
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private GameObject breathPrefab;

    [SerializeField]
    private float breathSpeed;
    [SerializeField]
    private float fireInterval;

    private Vector3 forwardDirection;
    private float lastFireTime;

    [SerializeField]
    private TerrorBringerAttackController terrorBringer;

    void Update()
    {
        ShootBreath();
        ShootBreath2();
    }

    private void ShootBreath()
    {
        if (terrorBringer.isBreath)
        {
            Vector3 forwardDirection = new Vector3(-headTransform.right.x, -0.25f, -headTransform.right.z);

            if (Time.time > lastFireTime + fireInterval)
            {
                FireBreath(forwardDirection, breathSpeed);
                lastFireTime = Time.time;
            }
        }
    }

    private void ShootBreath2()
    {
        if (terrorBringer.isSkillBreath)
        {
            Vector3 direction = (playerTransform.position - headTransform.position).normalized;

            if (Time.time > lastFireTime + fireInterval)
            {
                FireBreath(direction, breathSpeed * 2);
                lastFireTime = Time.time;
            }
        }
    }

    private void FireBreath(Vector3 direction, float breathSpeed)
    {
        Vector3 firePosition = headTransform.position + direction * 2f;

        GameObject breath = Instantiate(breathPrefab, firePosition, Quaternion.LookRotation(direction));
        Rigidbody rb = breath.GetComponent<Rigidbody>();

        rb.velocity = direction * breathSpeed;

        Destroy(breath, 3f);
    }
}