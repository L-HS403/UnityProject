using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrailOffset//����ȭ�Ϸ��� ��¿ �� ������
{
    public Vector3[] offsets;
}

public class TrailMove : MonoBehaviour
{
    [SerializeField]
    public TrailOffset[] trailOffsets;
    public float waitTime;
    private TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;
    }

    public void TrailOn(int num)
    {
        StartCoroutine(MoveTrail(num));
    }

    IEnumerator MoveTrail(int num)
    {
        if (num >= trailOffsets.Length)
        {
            Debug.LogWarning("Invalid Trail Number: " + num + "\t(Max " + (trailOffsets.Length - 1) + ")");
            yield break;
        }

        trail.emitting = true;

        for (int i = 0; i < trailOffsets[num].offsets.Length - 1; i++)
        {
            Vector3 startPos = trailOffsets[num].offsets[i];
            Vector3 endPos = trailOffsets[num].offsets[i + 1];
            float elapsedTime = 0f;

            while (elapsedTime < waitTime)
            {
                trail.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null; // �� �����Ӹ��� ������Ʈ
            }

            // �� ���� ��Ȯ�� ���߱�
            trail.transform.localPosition = endPos;
        }

        yield return new WaitForSeconds(waitTime / 2);
        trail.emitting = false;
    }
}
