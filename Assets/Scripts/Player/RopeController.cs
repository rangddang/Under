using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject hook;

    public bool isOnRope;

    private void Update()
    {
        
    }

    public void OnRope()
    {
        isOnRope = true;
        line.gameObject.SetActive(true);
        hook.SetActive(true);
    }

    public void OffRope()
    {
        isOnRope = false;
        line.gameObject.SetActive(false);
        hook.SetActive(false);
    }
}
