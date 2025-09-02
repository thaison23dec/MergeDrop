using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public bool isChoosingFruitToDestroy;
    public int smallFruitItemNumber = 2;
    public int chosenFruitItemNumber = 2;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        isChoosingFruitToDestroy = false;
    }

    public void AddSmallFruitItem()
    {
        smallFruitItemNumber++;
    }

    public void AddChosenFruitItem()
    {
        chosenFruitItemNumber++;
    }

    public void DestroySmallFruit()
    {
        //if (smallFruitItemNumber <= 0) return;
        List<GameObject> currentFruitList = FruitHolder.instance.fruits;
        foreach(GameObject f in currentFruitList)
        {
            if(f.GetComponent<MergeObject>().prefabID <= 2)
            {
                f.GetComponent<MergeObject>().ParticleMergeFruit();
                Destroy(f.gameObject);
            }
        }
    }

    public void DestroyChosenFruit()
    {
        //if (chosenFruitItemNumber <= 0) return;
        UIManager.instance.isOpeningPanel = true;
        GamePlayManager.Instance.InActivatePointerDragRangeCollider();
        isChoosingFruitToDestroy = true;
        List<GameObject> currentFruitList = FruitHolder.instance.fruits;
        foreach (GameObject f in currentFruitList)
        {
            f.gameObject.layer = LayerMask.NameToLayer("MergeObject");
        }
    }

    public void EndDestroyChosenFruit()
    {
        UIManager.instance.isOpeningPanel = false;
        UIManager.instance.CloseTextPopUp();
        isChoosingFruitToDestroy = false;
        List<GameObject> currentFruitList = FruitHolder.instance.fruits;
        foreach (GameObject f in currentFruitList)
        {
            f.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
}
