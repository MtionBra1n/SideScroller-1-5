using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_gameObject : MonoBehaviour
{
    public GameObject objectA;

    private void Start()
    {
        print(objectA.name);
    }
}
