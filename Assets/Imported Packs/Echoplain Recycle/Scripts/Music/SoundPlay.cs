using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public string _soundName;
    public string _targetTag;
    public bool _music = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_targetTag != "")
        {
            if (collision.gameObject.CompareTag(_targetTag))
            {
                PlaySoundTrack();
            }
        }
    }

    public void PlaySoundTrack()
    {
        if (_music)
        {
            MusicManager.Instance.PlayMusic(_soundName);
        }
        else
            SoundManager.Instance.PlaySound2D(_soundName);
    }
}
