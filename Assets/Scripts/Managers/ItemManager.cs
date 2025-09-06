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
        bool found = false;
        List<GameObject> currentFruitList = FruitHolder.instance.fruits;
        foreach(GameObject f in currentFruitList)
        {
            if(f.GetComponent<MergeObject>().prefabID <= 2)
            {
                found = true;
                f.GetComponent<MergeObject>().ParticleMergeFruit();
                Destroy(f.gameObject);
            }
        }
        if (!found)
        {
            UIManager.instance.ShowNotification("No Small Fruit To Destroy");
            Debug.Log("ShowNotify");
        } else
        {
            TicketManager.instance.DecreaseTicket();
        }
    }

    public void DestroyChosenFruit()
    {
        //if (chosenFruitItemNumber <= 0) return;
        List<GameObject> currentFruitList = FruitHolder.instance.fruits;
        if(currentFruitList.Count == 0)
        {
            UIManager.instance.ShowNotification("No Fruit To Destroy");
            UIManager.instance.isOpeningPanel = false;
            return;
        }
        UIManager.instance.isOpeningPanel = true;
        UIManager.instance.OpenTextPopUp();
        GamePlayManager.Instance.InActivatePointerDragRangeCollider();
        isChoosingFruitToDestroy = true;
        foreach (GameObject f in currentFruitList)
        {
            f.gameObject.layer = LayerMask.NameToLayer("MergeObject");
        }
        TicketManager.instance.DecreaseTicket();
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
