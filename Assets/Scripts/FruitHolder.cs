using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHolder : MonoBehaviour
{
    public static FruitHolder instance;

    [SerializeField] private List<GameObject> fruits = new List<GameObject>();


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

    public void Save(ref SceneFruitData data)
    {
        List<FruitSaveData> fruitSaveDataList = new List<FruitSaveData>();

        for(int i = fruits.Count - 1; i >= 0; i--)
        {
            GameObject fruit = fruits[i];
            FruitSaveData saveData = new FruitSaveData
            {
                Position = fruit.transform.position,
                PrefabId = GamePlayManager.Instance.objectList.IndexOf(fruit.gameObject)
            };

            fruitSaveDataList.Add(saveData);
        }

        data.Fruits = fruitSaveDataList.ToArray();
    }

    public void Load(SceneFruitData data)
    {
        foreach(var fruit in fruits)
        {
            if (fruit != null) Destroy(fruit);
        }

        fruits.Clear();

        foreach(var fruitData in data.Fruits)
        {
            GameObject prefab = GamePlayManager.Instance.objectList[fruitData.PrefabId];
            GameObject fruit = Instantiate(prefab, fruitData.Position, Quaternion.identity);
            fruits.Add(fruit);
        }
    }

}

[System.Serializable]

public struct SceneFruitData
{
    public FruitSaveData[] Fruits;
}

[System.Serializable]
public struct FruitSaveData
{
    public Vector3 Position;
    public int PrefabId;
}
