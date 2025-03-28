using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject progressBar;
    public GameObject transitionsContainer;

    private SceneTransition[] transitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FirstLaunch()
    {
        if (PlayerPrefs.GetInt("Level") < 4 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            LoadSceneLevel("Instant", 0);
        }
        if (PlayerPrefs.GetInt("Level") > 3 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadSceneLevel("Instant", 1);
        }
    }

    private void Start()
    {
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
        FirstLaunch();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadMenu();
            }
        }
    }

    public void LoadMenu()
    {
        if (PlayerPrefs.GetInt("Level") > 3)
        {
            LoadSceneLevel("CrossFade", 1);
        }
        else
            LoadSceneLevel("CrossFade", 0);
    }

    public void LoadSceneName(string transitionName, string sceneName)
    {
        GetComponent<Canvas>().sortingOrder = 200;

        StartCoroutine(LoadSceneAsync(transitionName, sceneName, 0));
    }
    public void LoadSceneLevel(string transitionName, int level)
    {
        GetComponent<Canvas>().sortingOrder = 200;

        StartCoroutine(LoadSceneAsync(transitionName, null, level));
    }

    private IEnumerator LoadSceneAsync(string transitionName, string sceneName, int level)
    {
        SceneTransition transition = transitions.First(t => t.name == transitionName);
        AsyncOperation scene;

        if (sceneName != null)
            scene = SceneManager.LoadSceneAsync(sceneName);
        else
            scene = SceneManager.LoadSceneAsync(level);

        scene.allowSceneActivation = false;

        yield return transition.AnimateTransitionIn();

        progressBar.gameObject.SetActive(true);

        do
        {
            //progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);

        yield return new WaitForSeconds(1f);

        scene.allowSceneActivation = true;

        progressBar.gameObject.SetActive(false);

        yield return transition.AnimateTransitionOut();

        yield return new WaitForSeconds(0.3f);

        GetComponent<Canvas>().sortingOrder = 0;
    }
}
