using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public Transform target;

    [SerializeField] private float cameraSpeed;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, cameraSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
