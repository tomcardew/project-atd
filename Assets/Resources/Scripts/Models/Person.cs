[System.Serializable]
public class Person
{
    public string name;
    public Populates population;
    public float rest; // working substracts rest, resting adds rest
    public float hunger; // hunger develops over time, eating reduces it
    public int foodConsumption; // food consumption rate

    public Person(string name, Populates population, int foodConsumption = 1)
    {
        this.name = name;
        this.population = population;
        rest = 1f;
        hunger = 0f;
        this.foodConsumption = foodConsumption;
    }
}
