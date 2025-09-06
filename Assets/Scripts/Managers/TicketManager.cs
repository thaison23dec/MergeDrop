using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    public static TicketManager instance;

    public int currentTicketNumber;

    private const string TicketNumberKey = "TicketNumber";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentTicketNumber = PlayerPrefs.GetInt(TicketNumberKey, 0);
    }

    public void UseTicket(ItemType itemType)
    {
        if (currentTicketNumber == 0)
        {
            UIManager.instance.ShowNotification("Not Enough Ticket");
            return;
        }

        switch (itemType)
        {
            case ItemType.SmallFruit:
                ItemManager.instance.DestroySmallFruit();
                break;

            case ItemType.ChosenFruit:
                ItemManager.instance.DestroyChosenFruit();
                break;
        }
    }

    public void UseTicketSmallFruit()
    {
        UseTicket(ItemType.SmallFruit);
    }

    public void UseTicketChosenFruit()
    {
        UseTicket(ItemType.ChosenFruit);
    }

    public void DecreaseTicket()
    {
        if (currentTicketNumber == 0) return;
        currentTicketNumber--;
        UIManager.instance.UpdateTicketNumber();
        SaveTicket();
    }

    public void IncreaseTicket()
    {
        currentTicketNumber++;
        UIManager.instance.UpdateTicketNumber();
        SaveTicket();
    }

    private void SaveTicket()
    {
        PlayerPrefs.SetInt(TicketNumberKey, currentTicketNumber);
        PlayerPrefs.Save();
    }
}
