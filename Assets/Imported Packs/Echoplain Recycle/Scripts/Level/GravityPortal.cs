using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPortal : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D lineMaterial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D _rb = collision.GetComponent<Rigidbody2D>();

            _rb.gravityScale *= -1;
        }
        else if (collision.CompareTag("Line"))
        {
            Rigidbody2D _rb = collision.GetComponent<Rigidbody2D>();

            if (collision.sharedMaterial == lineMaterial)
            {
                _rb.gravityScale *= -1;
            }
        }
    }
}
