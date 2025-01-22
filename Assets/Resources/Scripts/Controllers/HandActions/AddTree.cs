using UnityEngine;

public class AddTree : MonoBehaviour
{
    private void Start()
    {
        string[] names = new string[]
        {
            Cards.SmallTrees.name,
            Cards.MediumTrees.name,
            Cards.LargeTrees.name
        };
        int index = -1;
        int count = 0;
        while (index < 0 && count < 6)
        {
            var name = names[Random.Range(0, names.Length)];
            index = Manager.Cards.FindIndexOfCardWithNameOnDeck(name);
            count++;
        }
        if (index > -1)
        {
            Manager.Cards.MoveDeckIndexToHand(index);
        }
        Destroy(gameObject);
    }
}
