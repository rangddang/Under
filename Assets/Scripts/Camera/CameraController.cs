using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerHook playerHook;
    private Camera myCamera;

    private float currentSize;
    private Vector3 currentCameraPos;

    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Vector3 cameraPos;
    [SerializeField] private float baseSize = 7;
    [SerializeField] private float zoomSize = 5;
    [SerializeField] private float zoomSpeed = 5;
    [SerializeField] private float duration = 1;
    [SerializeField] private float magnitude = 5;

    private void Awake()
    {
        currentSize = baseSize;
        currentCameraPos = cameraPos;
        playerHook = target.gameObject.GetComponent<PlayerHook>();
        myCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + currentCameraPos, cameraSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (playerHook.IsHook)
        {
            currentSize = Mathf.Lerp(currentSize, zoomSize, Time.fixedDeltaTime * zoomSpeed);
        }
        else
        {
			currentSize = Mathf.Lerp(currentSize, baseSize, Time.fixedDeltaTime * zoomSpeed);
		}
		myCamera.orthographicSize = currentSize;
	}

	public void ShakeCamera()
    {
        StartCoroutine("Shake");
    }

    private IEnumerator Shake()
    {
        float elspeed = 0f;

        while (elspeed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;


            transform.position = target.position + currentCameraPos + new Vector3(x, y, 0);
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);

			elspeed += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
		}

        transform.position = target.position + currentCameraPos;
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	//private IEnumerator RestoreCamera()
	//{
	//    while (Vector3.Distance(currentCameraPos, cameraPos) > 0.1f)
	//    {
	//        currentCameraPos = Vector3.Lerp(currentCameraPos, cameraPos, restoreSpeed * Time.fixedDeltaTime);
	//        yield return new WaitForFixedUpdate();
	//    }
	//    currentCameraPos = cameraPos;
	//}
}
