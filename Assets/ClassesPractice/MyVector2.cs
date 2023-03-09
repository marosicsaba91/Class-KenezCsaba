using System;
using UnityEngine;

struct MyVector2
{
    public float x, y;

    public MyVector2(float x, float y) 
    {
        this.x = x; 
        this.y = y;
    }

    public float Magnitude
    {
        get => Mathf.Sqrt(x * x + y * y);
    }
}