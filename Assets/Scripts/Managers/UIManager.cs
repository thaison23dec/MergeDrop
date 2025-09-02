using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject item1Panel;
    [SerializeField] private GameObject item2Panel;
    [SerializeField] private Image nextFruitImage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text popUpText;

    public bool isOpeningPanel = false;

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
        isOpeningPanel = true;
        optionPanel.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        GamePlayManager.Instance.canDrag = false;
        //PauseManager.instance.PauseGame();
    }

    public void CloseMenu()
    {
        isOpeningPanel = false;
        optionPanel.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        GamePlayManager.Instance.canDrag = true;
        //PauseManager.instance.UnpauseGame();
    }

    public void OpenItemPanel(GameObject itemPanel)
    {
        isOpeningPanel = true;
        itemPanel.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        GamePlayManager.Instance.canDrag = false;
    }

    public void CloseItemPanel(GameObject itemPanel)
    {
        isOpeningPanel = false;
        itemPanel.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        GamePlayManager.Instance.canDrag = true;
        if (popUpText.isActiveAndEnabled)
        {
            CloseTextPopUp();
        }
    }

    public void CloseItem2Panel(GameObject itemPanel)
    {
        itemPanel.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        GamePlayManager.Instance.canDrag = true;
    }

    public void OpenTextPopUp()
    {
        popUpText.gameObject.SetActive(true);
    }

    public void CloseTextPopUp()
    {
        popUpText.gameObject.SetActive(false);
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        PauseManager.instance.PauseGame();
    }

    public void Restart()
    {
        Loader.Load(Loader.Scene.InGame);
        GameManager.instance.currentGameState = GameManager.GameState.NewGame;
    }

    public void ContinueLastSavedGame()
    {
        if(GameManager.instance.currentGameState == GameManager.GameState.GameOver)
        {
            Restart();
        } else
        {
            Loader.Load(Loader.Scene.InGame);
            GameManager.instance.currentGameState = GameManager.GameState.Continue;
        }
    }

    public void LoadMainMenuScene()
    {
        SaveSystem.Save();
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void UpdateScore()
    {
        scoreText.text = ScoreManager.instance.currentScore.ToString();
    }

    public void ShowNextFruitImage()
    {
        int id = GamePlayManager.Instance.nextFruitIndex;
        nextFruitImage.sprite = GamePlayManager.Instance.objectList[id].GetComponent<SpriteRenderer>().sprite;
    }
}
