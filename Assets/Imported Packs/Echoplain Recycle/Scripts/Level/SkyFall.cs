using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFall : MonoBehaviour
{
    private GameObject _player;
    public Vector3 _offset = new Vector3(0, 20);

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    public void MovePosition()
    {
        _player.transform.position = _player.transform.position + _offset;
        _player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
