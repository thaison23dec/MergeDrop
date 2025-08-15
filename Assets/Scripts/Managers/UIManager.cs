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
    [SerializeField] private Image nextFruitImage;
    [SerializeField] private TMP_Text scoreText;

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
        optionPanel.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        PauseManager.instance.PauseGame();
    }

    public void CloseMenu()
    {
        optionPanel.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        PauseManager.instance.UnpauseGame();
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        PauseManager.instance.PauseGame();
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
