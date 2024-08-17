using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject deadUI;
    public GameObject clearUI;

    private bool isPause;

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
    }

    public void Die()
    {
        deadUI.SetActive(true);
        ActivePause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false)
            {
                ActivePause();
                return;
            }

            if (isPause == true)
            {
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
}