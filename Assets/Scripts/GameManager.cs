using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Score
{
    public int[] timeScore;
    public float[] damageScore;
    public float[] score;
    public float totalScore;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject deadUI;
    public GameObject clearUI;
    public GameObject countinue;
    public GameObject pauseUI;
    public GameObject pauseCountinue;
    public GameObject allClearUI;

    private bool isPause;

    public Score score;

    public float receivedDamage;

    [SerializeField]
    private int life = 3;
    private int currentLife;

    public int currentPotionCount;

    private void Start()
    {
        ResetLife();
    }

    void Update()
    {
        Pause();
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    instance = new GameManager();
            }
            return instance;
        }
    }

    public IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!AsyncLoad.isDone)
        {
            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindUIObject();
        DisactivePause();
    }

    private void FindUIObject()
    {
        deadUI = GameObject.Find("DeadUI").transform.Find("DeadUIActivate").gameObject;
        clearUI = GameObject.Find("ClearUI").transform.Find("ClearUIActivate").gameObject;
        countinue = GameObject.Find("DeadUI").transform.Find("Countinue").gameObject;
        pauseUI = GameObject.Find("PauseUI").transform.Find("PauseUIActivate").gameObject;
        pauseCountinue = GameObject.Find("PauseUI").transform.Find("Countinue").gameObject;
        allClearUI = GameObject.Find("ClearUI").transform.Find("AllClearUI").gameObject;
    }

    public void Die()
    {
        deadUI.SetActive(true);
        CursorUnlock();
        Timer.Instance.StopTimer();
        if (currentLife > 0)
        {
            countinue.SetActive(true);
        }
        ActivePause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false)
            {
                pauseUI.SetActive(true);
                if (currentLife > 0)
                {
                    pauseCountinue.SetActive(true);
                }
                CursorUnlock();
                ActivePause();
                return;
            }

            if (isPause == true)
            {
                pauseUI.SetActive(false);
                if (currentLife > 0)
                {
                    pauseCountinue.SetActive(false);
                }
                DisactivePause();
                CursorLock();
                return;
            }
        }
    }

    public void ActivePause()
    {
        Time.timeScale = 0;
        isPause = true;
    }

    public void DisactivePause()
    {
        Time.timeScale = 1;
        isPause = false;
    }

    public void DecreaseLife()
    {
        currentLife--;
    }

    public void ResetLife()
    {
        currentLife = life;
    }

    public int GetLife()
    {
        return life;
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public void CursorLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorUnlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameAllClear()
    {
        clearUI.SetActive(false);
        allClearUI.SetActive(true);
        score.totalScore = score.score[0] + score.score[1] + score.score[2] + score.score[3];
        ActivePause();
    }
}