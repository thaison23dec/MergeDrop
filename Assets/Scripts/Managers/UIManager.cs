using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

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

}
