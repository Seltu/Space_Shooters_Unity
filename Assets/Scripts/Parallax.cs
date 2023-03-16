using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float height;
    
    public void Scroll()
    {
        float delta = -scrollSpeed * Time.deltaTime;
        transform.position += new Vector3(0f, delta, 0f);
    }

    private void Update()
    {
        Scroll();
        if (transform.position.y < -30.5) transform.position = new Vector3(0f, 60f, 0f);
    }
}
