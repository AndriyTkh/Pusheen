using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    public Transform respawnPoint;

    private GameObject player;

    public GameObject particl;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = respawnPoint.position;
        }
    }

    public void DeathTransition()
    {
        Instantiate(particl, player.transform.position, Quaternion.identity);
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
