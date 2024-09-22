using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float shakeRange;
    [SerializeField]
    private float duration;

    public Vector3 originalPos;
    public Vector3 originPos;

    public float positionChangeSpeed = 1.0f;  // 위치 변경 속도
    public float rotationChangeSpeed = 1.0f;  // 회전 변경 속도

    private Vector3 targetPosition;  // 목표 위치
    private Vector3 targetRotation;  // 목표 회전 각도

    private bool isChanging = false;

    private void Start()
    {
        originalPos = transform.localPosition;
        originPos = originalPos;
        targetPosition = transform.localPosition;
        targetRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        ChangeCamera();
    }

    public void CameraShake()
    {
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    private void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = transform.localPosition;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        transform.localPosition = cameraPos;
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        transform.localPosition = originPos;
    }

    public void ChangeCameraTransform(float pos, float rot)
    {
        targetPosition = new Vector3(transform.localPosition.x, pos, transform.localPosition.z);
        targetRotation = new Vector3(rot, transform.localEulerAngles.y, transform.localEulerAngles.z);

        isChanging = true;
    }

    public void ResetCameraTransform()
    {
        targetPosition = new Vector3(transform.localPosition.x, 2.2f, transform.localPosition.z);
        targetRotation = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);

        isChanging = true;
    }

    private void ChangeCamera()
    {
        if (isChanging)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * positionChangeSpeed);

            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.x = Mathf.LerpAngle(currentRotation.x, targetRotation.x, Time.deltaTime * rotationChangeSpeed);
            transform.localEulerAngles = currentRotation;

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f &&
                Mathf.Abs(currentRotation.x - targetRotation.x) < 0.1f)
            {
                transform.localPosition = targetPosition;
                transform.localEulerAngles = targetRotation;
                isChanging = false;
            }
        }
    }
}
