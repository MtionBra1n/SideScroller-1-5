using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTest : MonoBehaviour
{
    public GameObject objectA;
    public SpriteRenderer objectB;
    public GameObject objectC;

    private void Start()
    {
        // greife auf Objekt A
        // zu und verändere die
        // Farbe des Sprite Rednerers auf rot
        objectA.GetComponent<SpriteRenderer>().color = Color.red;
        print(gameObject.name);
        objectB.color = Color.black;
        objectB.gameObject.transform.position = Vector3.zero;
        
    }
}
