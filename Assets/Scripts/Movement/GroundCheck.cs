using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool OnGround;
    public Vector2 ContactNormal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
    }

    public void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactNormal = collision.GetContact(i).normal;

            //Checking for ROUNDED collision

            OnGround |= Mathf.Abs(ContactNormal.y) >= 0.8f;
        }
    }
}
