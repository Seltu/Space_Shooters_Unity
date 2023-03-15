using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
[Serializable]
public struct Wave
{
    public int enemy;
    public int number;
    public int curve;
    public Vector2 offset;

    public Wave(int enemy, int number, int curve)
    {
        this.enemy = enemy;
        this.number = number;
        this.curve = curve;
        offset = new Vector2();
    }
    
    public Wave(int enemy, int number, int curve, int offsetX, int offsetY)
    {
        this.enemy = enemy;
        this.number = number;
        this.curve = curve;
        offset = new Vector2(offsetX, offsetY);
    }
}
