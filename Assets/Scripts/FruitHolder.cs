using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHolder : MonoBehaviour
{
    public static FruitHolder instance;

    [SerializeField] public List<GameObject> fruits = new List<GameObject>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddFruit(GameObject fruit)
    {
        if (!fruits.Contains(fruit))
        {
            fruit.transform.SetParent(transform);
            fruits.Add(fruit);
            CleanAndSort();
        }
    }

    public void RemoveFruit(GameObject fruit)
    {
        if (fruits.Contains(fruit))
        {
            fruits.Remove(fruit);
            CleanAndSort();
        }
    }

    public void CleanAndSort()
    {
        fruits.RemoveAll(fruit => fruit == null);
        fruits.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
    }

}
