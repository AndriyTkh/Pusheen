using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float _reloadCount;
    public float _reloadMax;

    public GameObject _linkPortal;

    private void FixedUpdate()
    {
        if (_reloadCount >= 0)
        {
            _reloadCount -= Time.fixedDeltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Line"))
        {
            if (_reloadCount < 0)
            {
                _linkPortal.GetComponent<Teleport>()._reloadCount = _reloadMax;
                collision.transform.position = _linkPortal.transform.position;

                _reloadCount = _reloadMax;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Line"))
        {
            if (_reloadCount < 0)
            {
                _linkPortal.GetComponent<Teleport>()._reloadCount = _reloadMax;
                collision.transform.position = _linkPortal.transform.position;

                _reloadCount = _reloadMax;
            }
        }
    }
}
