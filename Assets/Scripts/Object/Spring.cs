using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float SpringPower = 30f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (collision.gameObject.transform.position.y - 1) > transform.position.y)
        {
            Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            rigidbody.velocity = new Vector3(rigidbody.velocity.x, SpringPower, 0);
        }
    }
}
