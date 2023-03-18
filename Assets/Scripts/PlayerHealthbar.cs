using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    public GameObject playerShip;
    public Sprite[] sprite;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite[playerShip.GetComponent<PlayerShip>().hp];
    }

    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite[playerShip.GetComponent<PlayerShip>().hp];
    }

    public void PlayerDead()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite[0];
    }
}
