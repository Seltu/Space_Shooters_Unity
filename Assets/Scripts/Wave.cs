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

    public Wave(int enemy, int number, int curve)
    {
        this.enemy = enemy;
        this.number = number;
        this.curve = curve;
    }
}
