using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Image nextFruitImage;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ShowNextFruitImage();
    }

    public void OpenMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        PauseManager.instance.PauseGame();
    }

    public void CloseMenu()
    {
        menuCanvas.gameObject.SetActive(false);
        PauseManager.instance.UnpauseGame();
    }

    public void ShowNextFruitImage()
    {
        int id = GamePlayManager.Instance.nextFruitIndex;
        nextFruitImage.sprite = GamePlayManager.Instance.objectList[id].GetComponent<SpriteRenderer>().sprite;
    }
}
