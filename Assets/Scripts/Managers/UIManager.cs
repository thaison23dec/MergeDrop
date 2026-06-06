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
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private TextMeshProUGUI ticketNumberText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private float fadeDuration = 2f;

    public bool isOpeningPanel = false;
    private Coroutine currentRoutine;

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
        UpdateTicketNumber();
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

    public void ShowNotification(string message)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(FadeText(message));
    }

    private IEnumerator FadeText(string message)
    {
        notificationText.text = message;

        Color color = notificationText.color;
        color.a = 1f;
        notificationText.color = color;

        yield return new WaitForSeconds(1f);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            color.a = alpha;
            notificationText.color = color;
            yield return null;
        }

        color.a = 0f;
        notificationText.color = color;
        currentRoutine = null;
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

    public void UpdateTicketNumber()
    {
        ticketNumberText.text = "x" + TicketManager.instance.currentTicketNumber.ToString();
    }

    public void UpdateGameOverScore()
    {
        gameOverScoreText.text = "Score: " + ScoreManager.instance.currentScore.ToString();
    }

    public void ShowNextFruitImage()
    {
        int id = GamePlayManager.Instance.nextFruitIndex;
        nextFruitImage.sprite = GamePlayManager.Instance.objectList[id].GetComponent<SpriteRenderer>().sprite;
    }
}
