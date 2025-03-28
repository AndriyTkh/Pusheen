using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneTransition : MonoBehaviour
{
    private int _level;
    public string _transitionName;

    private void Start()
    {
        _level = SceneManager.GetActiveScene().buildIndex;
    }

    public void Transition()
    {
        PlayerPrefs.SetInt("Level", _level + 1);
        LevelManager.Instance.LoadSceneLevel(_transitionName, _level + 1);
    }
}
