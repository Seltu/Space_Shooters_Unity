using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAxis
{
    private readonly KeyCode _positive;
    private readonly KeyCode _negative;
    private KeyCode _lastPressed;

    public ButtonAxis(KeyCode positive, KeyCode negative)
    {
        _positive = positive;
        _negative = negative;
    }
    
    public void Check()
    {
        if(Input.GetKeyDown(_positive))
        {
            _lastPressed = _positive;
        }
        if(Input.GetKeyDown(_negative))
        {
            _lastPressed = _negative;
        }
        if (Input.GetKeyUp(_positive) && Input.GetKey(_negative))
        {
            _lastPressed = _negative;
        }
        if (Input.GetKeyUp(_negative) && Input.GetKey(_positive))
        {
            _lastPressed = _positive;
        }
    }

    public float GetAxis()
    {
        if (Input.GetKey(_positive) && (_lastPressed == KeyCode.None || _lastPressed == _positive))
        {
            return 1f;
        }
        if (Input.GetKey(_negative) && (_lastPressed == KeyCode.None || _lastPressed == _negative))
        {
           return -1f;
        }
        return 0;
    }
}
