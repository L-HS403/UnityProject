using System.Collections;
using UnityEngine;

public class UsurperBreath : MonoBehaviour
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
    private UsurperAttackController usurper;

    void Update()
    {
        ShootBreath();
        ShootBreath2();
    }

    private void ShootBreath()
    {
        if (usurper.isBreath)
        {
            Vector3 forwardDirection = new Vector3(headTransform.forward.x, -0.1f, headTransform.forward.z);

            if (Time.time > lastFireTime + fireInterval)
            {
                FireBreath(forwardDirection, breathSpeed);
                lastFireTime = Time.time;
            }
        }
    }

    private void ShootBreath2()
    {
        if (usurper.isSkillBreath)
        {
            Vector3 direction = (playerTransform.position - headTransform.position).normalized;

            if (Time.time > lastFireTime + fireInterval * 2)
            {
                FireBreath(direction, breathSpeed * 2.5f);
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

        Destroy(breath, 2f);
    }
}
