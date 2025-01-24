using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Populates : MonoBehaviour
{
    // Public properties
    public int population;
    public float restRecovery = 0.1f;
    public float restRecoveryMultiplier = 1.0f;

    // Private properties
    private List<string> names = new List<string>
    {
        "Alice",
        "Bob",
        "Charlie",
        "Diana",
        "Eve",
        "Frank",
        "Grace",
        "Hank",
        "Ivy",
        "Jack",
        "Karen",
        "Leo",
        "Mona",
        "Nina",
        "Oscar",
        "Paul",
        "Quincy",
        "Rachel",
        "Steve",
        "Tina",
        "Uma",
        "Victor",
        "Wendy",
        "Xander",
        "Yara",
        "Zane",
        "Aaron",
        "Bella",
        "Cameron",
        "Derek",
        "Elena",
        "Fiona",
        "George",
        "Holly",
        "Ian",
        "Jasmine",
        "Kyle",
        "Liam",
        "Megan",
        "Nathan",
        "Olivia",
        "Peter",
        "Quinn",
        "Riley",
        "Sophie",
        "Tom",
        "Ursula",
        "Violet",
        "Will",
        "Xenia",
        "Yvonne",
        "Zach"
    };
    private Coroutine restRoutine;

    [SerializeField]
    private List<Person> people;

    // Computed property to get the current generation quantity
    private float CurrentRestRecovery
    {
        get { return restRecovery * restRecoveryMultiplier; }
    }

    private void Start()
    {
        // Initialize the people list and populate it with random names
        people = new List<Person>();
        for (int i = 0; i < population; i++)
        {
            string name = names[Random.Range(0, names.Count)];
            people.Add(new Person(name, this));
        }
        // Start the rest routine coroutine
        restRoutine = StartCoroutine(RestRoutine());
    }

    private void OnDisable()
    {
        // Stop the rest routine coroutine if it is running
        if (restRoutine != null)
        {
            StopCoroutine(restRoutine);
            restRoutine = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the person is going home and add them to the people list
        var person = other.GetComponentInParent<PersonMovable>();
        if (person.isGoingHome && people.Count < population)
        {
            AddPerson(person.person);
            Destroy(other.transform.parent.gameObject);
        }
    }

    public void AddPerson(Person person)
    {
        // Add a person to the people list
        people.Add(person);
    }

    private IEnumerator RestRoutine()
    {
        // Continuously update the rest status of people every second
        while (true)
        {
            UpdateRestStatus();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRestStatus()
    {
        // Update the rest status of each person and move them to a resource generator if fully rested
        ResourceGenerator generator = Utils.GetAvailableResource(transform.position);
        for (int i = 0; i < people.Count; i++)
        {
            Person person = people[i];
            person.rest += CurrentRestRecovery;
            if (person.rest >= 1)
            {
                person.rest = 1;
                if (generator != null)
                {
                    GameObject _person = Instantiate(
                        Prefabs.GetPrefab(Prefabs.UnitType.Person),
                        transform.position,
                        Quaternion.identity
                    );
                    PersonMovable p = _person.GetComponent<PersonMovable>();
                    p.person = person;
                    p.target = generator.transform.position;
                    people.RemoveAt(i);
                }
                else
                {
                    Debug.LogWarning("No available resource generator found");
                }
            }
        }
    }
}
