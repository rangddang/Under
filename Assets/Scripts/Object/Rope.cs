using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Transform player;
    private Transform hook;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
		hook = FindObjectOfType<Hook>().GetComponent<Transform>();
	}

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, player.position);
		lineRenderer.SetPosition(1, hook.position);

	}
}
