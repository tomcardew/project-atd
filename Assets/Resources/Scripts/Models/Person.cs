using UnityEngine;

[System.Serializable]
public class Person
{
    public string name;
    public Populates population;
    public float rest;

    public Person(string name, Populates population)
    {
        this.name = name;
        this.population = population;
        this.rest = 0.5f;
    }
}
