[System.Serializable]
public class Person
{
    public string name;
    public Populates population;
    public float rest; // working substracts rest, resting adds rest

    public Person(string name, Populates population)
    {
        this.name = name;
        this.population = population;
        rest = 1f;
    }
}
