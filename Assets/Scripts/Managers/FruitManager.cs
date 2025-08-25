using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Save(ref SceneFruitData data)
    {
        if (GameManager.instance.currentGameState == GameManager.GameState.GameOver) return;
        if(this.gameObject != null)
        {
            List<FruitSaveData> fruitSaveDataList = new List<FruitSaveData>();

            for(int i = FruitHolder.instance.fruits.Count - 1; i >= 0; i--)
            {
                GameObject fruit = FruitHolder.instance.fruits[i];
                FruitSaveData saveData = new FruitSaveData
                {
                    Position = fruit.transform.position,
                    Rotation = fruit.transform.rotation,
                    PrefabId = fruit.GetComponent<MergeObject>().prefabID
                };

                fruitSaveDataList.Add(saveData);
            }

            data.Fruits = fruitSaveDataList.ToArray();
        }
    }

    public void Load(SceneFruitData data)
    {
        foreach(var fruit in FruitHolder.instance.fruits)
        {
            if (fruit != null) Destroy(fruit);
        }

        FruitHolder.instance.fruits.Clear();

        foreach(var fruitData in data.Fruits)
        {
            GameObject prefab = GamePlayManager.Instance.objectList[fruitData.PrefabId];
            GameObject fruit = Instantiate(prefab, fruitData.Position, fruitData.Rotation);
            fruit.GetComponent<MergeObject>().isDraggable = false;
            fruit.GetComponent<MergeObject>().isDropped = true;
            fruit.GetComponent<MergeObject>().rb.bodyType = RigidbodyType2D.Dynamic; 
            FruitHolder.instance.AddFruit(fruit);
        }
    }

    public void ClearData(ref SceneFruitData data)
    {
        data.Fruits = System.Array.Empty<FruitSaveData>();
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
    public Quaternion Rotation;
    public int PrefabId;
}
