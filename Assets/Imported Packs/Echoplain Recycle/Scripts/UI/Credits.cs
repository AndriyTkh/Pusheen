using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public float _maxHeight;
    public float _speed;
    private bool loaded = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * _speed * Time.deltaTime;

        if (transform.position.y > _maxHeight && !loaded)
        {
            loaded = true;
            LevelManager.Instance.LoadMenu();
        }
    }
}
