using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLight;
    public int maxLight;

    private void Awake()
    {
        maxLight = FindObjectsOfType<Light>().Length;
    }


}
