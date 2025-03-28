using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox") || collision.gameObject.CompareTag("DestroyBox"))
        {
            RespawnManager.Instance.DeathTransition();
        }
    }
}
