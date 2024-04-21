using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

}
