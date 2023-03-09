using System;

class Person
{
    public string name;     // null
    int born;               // 0
    float height;           // 0f
    bool isSmoking;         // false

    public Person(string name, int born, float height, bool isSmoking) 
    {
        this.name = name;
        Born = born;
        SetHeight(height);
        this.isSmoking = isSmoking;
    }

    public float GetHeight() => height;

    public void SetHeight(float h) => height = Math.Max(1, h);

    public int Born     // Property
    {
        get => born;
        set => born = Math.Clamp(value, 1800, 2023);
    }

}