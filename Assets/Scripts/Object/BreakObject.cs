using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField] private Light light;


    private void Start()
    {
		if (light != null)
		{
			light.SetTake(false);
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            gameObject.SetActive(false);
            CameraController cam = Camera.main.GetComponent<CameraController>();
            cam.ShakeCamera();

            if(light != null)
            {
                light.SetTake(true);
            }
        }
    }
}
