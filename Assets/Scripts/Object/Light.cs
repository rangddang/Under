using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
	private bool isTake = true;
	private GameManager gameManager;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	public void SetTake(bool boolen)
	{
		isTake = boolen;
		gameObject.SetActive(boolen);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Player") && isTake)
		{
			TakeLight();
		}
	}

	private void TakeLight()
	{
		gameManager.currentLight++;
		gameObject.SetActive(false);
	}
}
