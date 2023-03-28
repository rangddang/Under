using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HookTarget : MonoBehaviour
{
    private Hook hook;
    private CircleCollider2D collider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float RemoveTime = 5f;

    private void Awake()
    {
        hook = FindObjectOfType<Hook>();
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            hook.SetTarget(transform);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHook playerHook = collision.gameObject.GetComponent<PlayerHook>();
            if (playerHook.IsHook)
            {
                StartCoroutine("Remove");
            }
        }
    }

    private IEnumerator Remove()
    {
        collider.enabled = false;
        spriteRenderer.enabled = false;
		yield return new WaitForSeconds(RemoveTime);
		collider.enabled = true;
        spriteRenderer.enabled = true;
	}
}
