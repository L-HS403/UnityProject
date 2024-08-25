using UnityEngine;

public class SoulEaterShootBall : MonoBehaviour
{
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject ball2Prefab;

    [SerializeField]
    private float ballSpeed;

    [SerializeField]
    private SoulEaterAttackController soulEater;

    public void TryShootBall()
    {
        Vector3 direction = (playerTransform.position - headTransform.position).normalized;
        ShootBall(direction, ballSpeed);
    }

    private void ShootBall(Vector3 direction, float ballSpeed)
    {
        Vector3 firePosition = headTransform.position + direction * 2f;

        GameObject ball = Instantiate(ballPrefab, firePosition, Quaternion.LookRotation(direction));
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        rb.velocity = direction * ballSpeed;

        Destroy(ball, 3f);
    }

    public void TryShootBall2()
    {
        Vector3 direction = (playerTransform.position - headTransform.position).normalized;
        ShootBall2(direction, ballSpeed);
    }

    private void ShootBall2(Vector3 direction, float ballSpeed)
    {
        Vector3 firePosition = headTransform.position + direction * 2f;

        GameObject ball = Instantiate(ball2Prefab, firePosition, Quaternion.LookRotation(direction));
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        rb.velocity = direction * ballSpeed * 7;

        Destroy(ball, 3f);
    }
}
