using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject deadUI;
    public GameObject clearUI;
    public GameObject countinue;
    public GameObject pauseUI;
    public GameObject pauseCountinue;

    private bool isPause;

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
    }

    public void Die()
    {
        deadUI.SetActive(true);
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
}