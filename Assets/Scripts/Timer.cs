using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private static Timer instance;

    private bool timeActive = true;

    [SerializeField]
    private float LimitTime;
    public float currentTimeSec;
    public int currentTimeMin;

    public static Timer Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(Timer)) as Timer;

                if (instance == null)
                    instance = new Timer();
            }
            return instance;
        }
    }

    private void Start()
    {
        ResetTimer();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        SecToMin();
        DecreaseTime();
    }

    private void DecreaseTime()
    {
        if (timeActive)
            currentTimeSec -= Time.deltaTime;
    }

    private void SecToMin()
    {
        if (currentTimeSec >= 60f)
        {
            currentTimeMin += 1;
            currentTimeSec -= 60f;
        }

        if (currentTimeSec < 0f)
        {
            if (currentTimeMin == 0)
            {
                GameManager.Instance.Die();
            }
            else
            {
                currentTimeMin -= 1;
                currentTimeSec = 60f;
            }
        }
    }

    public void ResetTimer()
    {
        currentTimeMin = 0;
        currentTimeSec = LimitTime;
    }

    public void StopTimer()
    {
        timeActive = false;
    }

    public void StartTimer()
    {
        timeActive = true;
    }
}
