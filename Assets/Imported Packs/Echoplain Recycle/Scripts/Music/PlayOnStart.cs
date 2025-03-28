using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStart : MonoBehaviour
{
    public string _musicName;
    public bool _interapt = false;

    void Start()
    {
        if (_interapt)
            MusicManager.Instance.PlayMusic(_musicName);
        else if (MusicManager.Instance.musicSource.isPlaying == false)
        {
            MusicManager.Instance.PlayMusic(_musicName);
        }
    }


}
