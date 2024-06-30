using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEffect : MonoBehaviour
{
    public float destroytime;
    private void Start() 
    {
        Destroy(gameObject, destroytime);
    }
}
