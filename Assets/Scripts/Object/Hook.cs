using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
	private PlayerHook playerHook;
    private Transform target;
    public Transform Target => target;

	private void Awake()
	{
		playerHook = FindObjectOfType<PlayerHook>();
	}

	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void SetTarget(Transform trans)
	{
		target = trans;
	}

	public void RemoveTarget()
    {
        target = null;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.CompareTag("BreakObject"))
		{
			playerHook.StopHook();
		}
	}
}
